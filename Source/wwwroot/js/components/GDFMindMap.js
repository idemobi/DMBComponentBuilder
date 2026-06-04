(function () {
    "use strict";

    var minScale = 0.42;
    var maxScale = 2.4;

    function clamp(value, min, max) {
        return Math.max(min, Math.min(max, value));
    }

    function isVisible(element) {
        return !!(element && element.getClientRects().length);
    }

    function getCenter(element, origin) {
        var rect = element.getBoundingClientRect();

        return {
            x: rect.left - origin.left + rect.width / 2,
            y: rect.top - origin.top + rect.height / 2,
            left: rect.left - origin.left,
            right: rect.right - origin.left,
            top: rect.top - origin.top,
            bottom: rect.bottom - origin.top
        };
    }

    function getLineColor(canvas) {
        var styles = window.getComputedStyle(canvas.closest(".gdf-mind-map") || canvas);
        return styles.getPropertyValue("--gdf-mind-map-line").trim() || "rgba(90, 120, 150, .55)";
    }

    function getViewport(map) {
        return map.querySelector("[data-gdf-mind-map-viewport=\"true\"]");
    }

    function getStage(map) {
        return map.querySelector("[data-gdf-mind-map-stage=\"true\"]");
    }

    function getLineMode(map) {
        var viewport = getViewport(map);

        return map.getAttribute("data-gdf-mind-map-line-mode")
            || (viewport ? viewport.getAttribute("data-gdf-mind-map-line-mode") : null)
            || "rounded-orthogonal";
    }

    function getState(map) {
        if (!map._gdfMindMapState) {
            var viewport = getViewport(map);
            var initialScale = viewport ? Number.parseFloat(viewport.getAttribute("data-gdf-mind-map-initial-scale") || "1") : 1;

            map._gdfMindMapState = {
                scale: clamp(Number.isFinite(initialScale) ? initialScale : 1, minScale, maxScale),
                x: 0,
                y: 0,
                isDragging: false,
                dragStartX: 0,
                dragStartY: 0,
                originX: 0,
                originY: 0
            };
        }

        return map._gdfMindMapState;
    }

    function resizeCanvas(canvas, rect) {
        var ratio = window.devicePixelRatio || 1;
        var width = Math.max(1, Math.round(rect.width * ratio));
        var height = Math.max(1, Math.round(rect.height * ratio));

        if (canvas.width !== width || canvas.height !== height) {
            canvas.width = width;
            canvas.height = height;
        }

        canvas.style.width = rect.width + "px";
        canvas.style.height = rect.height + "px";

        return ratio;
    }

    function updateZoomLabel(map) {
        var state = getState(map);
        var label = map.querySelector("[data-gdf-mind-map-zoom-label=\"true\"]");

        if (label) {
            label.textContent = Math.round(state.scale * 100) + "%";
        }
    }

    function applyTransform(map) {
        var stage = getStage(map);
        var state = getState(map);

        if (!stage) {
            return;
        }

        stage.style.transform = "translate3d(" + state.x + "px, " + state.y + "px, 0) scale(" + state.scale + ")";
        updateZoomLabel(map);
        animateLines(map, 180);
    }

    function zoomAt(map, nextScale, point) {
        var viewport = getViewport(map);
        var state = getState(map);
        var oldScale = state.scale;
        var scale = clamp(nextScale, minScale, maxScale);

        if (!viewport || Math.abs(scale - oldScale) < 0.001) {
            return;
        }

        var rect = viewport.getBoundingClientRect();
        var x = point ? point.x - rect.left : rect.width / 2;
        var y = point ? point.y - rect.top : rect.height / 2;
        var ratio = scale / oldScale;

        state.x = x - (x - state.x) * ratio;
        state.y = y - (y - state.y) * ratio;
        state.scale = scale;

        applyTransform(map);
    }

    function resetView(map) {
        var state = getState(map);
        var viewport = getViewport(map);
        var initialScale = viewport ? Number.parseFloat(viewport.getAttribute("data-gdf-mind-map-initial-scale") || "1") : 1;

        state.scale = clamp(Number.isFinite(initialScale) ? initialScale : 1, minScale, maxScale);
        state.x = 0;
        state.y = 0;

        applyTransform(map);
    }

    function fitView(map) {
        var viewport = getViewport(map);
        var stage = getStage(map);
        var state = getState(map);

        if (!viewport || !stage) {
            return;
        }

        var viewportWidth = viewport.clientWidth;
        var viewportHeight = viewport.clientHeight;
        var stageWidth = Math.max(stage.offsetWidth, stage.scrollWidth);
        var stageHeight = Math.max(stage.offsetHeight, stage.scrollHeight);

        if (!viewportWidth || !viewportHeight || !stageWidth || !stageHeight) {
            return;
        }

        state.scale = clamp(Math.min((viewportWidth - 48) / stageWidth, (viewportHeight - 48) / stageHeight), minScale, 1.25);
        state.x = (viewportWidth - stageWidth * state.scale) / 2;
        state.y = (viewportHeight - stageHeight * state.scale) / 2;

        applyTransform(map);
    }

    function getBraceDistance(from, children, side, scale) {
        var startX = side === "left" ? from.left : from.right;
        var nearestChildX = children.reduce(function (current, child) {
            var edge = side === "left" ? child.right : child.left;

            return side === "left" ? Math.max(current, edge) : Math.min(current, edge);
        }, side === "left" ? Number.NEGATIVE_INFINITY : Number.POSITIVE_INFINITY);
        var available = Math.abs(nearestChildX - startX);

        if (!Number.isFinite(available) || available <= 0) {
            return 32 * scale;
        }

        return Math.min(clamp(available * .5, 30 * scale, 118 * scale), available / 2);
    }

    function getBraceSpineX(from, children, side, scale) {
        var startX = side === "left" ? from.left : from.right;
        var direction = side === "left" ? -1 : 1;

        return startX + direction * getBraceDistance(from, children, side, scale);
    }

    function drawStraightConnector(context, from, child, side) {
        var startX = side === "left" ? from.left : from.right;
        var endX = side === "left" ? child.right : child.left;

        context.moveTo(startX, from.y);
        context.lineTo(endX, child.y);
    }

    function drawOrthogonalConnector(context, from, child, side, spineX) {
        var startX = side === "left" ? from.left : from.right;
        var endX = side === "left" ? child.right : child.left;

        context.moveTo(startX, from.y);
        context.lineTo(spineX, from.y);
        context.lineTo(spineX, child.y);
        context.lineTo(endX, child.y);
    }

    function drawWeightedBezierConnector(context, from, child, side) {
        var direction = side === "left" ? -1 : 1;
        var startX = side === "left" ? from.left : from.right;
        var endX = side === "left" ? child.right : child.left;
        var margin = Math.abs(endX - startX);
        var force = margin * .5;

        context.moveTo(startX, from.y);
        context.bezierCurveTo(
            startX + direction * force,
            from.y,
            endX - direction * force,
            child.y,
            endX,
            child.y
        );
    }

    function drawRoundedOrthogonalConnector(context, from, child, side, scale, spineX) {
        var startX = side === "left" ? from.left : from.right;
        var startY = from.y;
        var endX = side === "left" ? child.right : child.left;
        var childY = child.y;
        var direction = side === "left" ? -1 : 1;
        var verticalDistance = Math.abs(childY - startY);
        var horizontalIn = Math.abs(spineX - startX);
        var horizontalOut = Math.abs(endX - spineX);
        var radius = Math.min(
            clamp(34 + scale * 10, 28, 56),
            verticalDistance / 2,
            horizontalIn / 2,
            horizontalOut / 2
        );

        if (verticalDistance < 1 || radius < 1) {
            drawWeightedBezierConnector(context, from, child, side);
            return;
        }

        var verticalDirection = childY >= startY ? 1 : -1;

        context.moveTo(startX, startY);
        context.lineTo(spineX - direction * radius, startY);
        context.quadraticCurveTo(spineX, startY, spineX, startY + verticalDirection * radius);
        context.lineTo(spineX, childY - verticalDirection * radius);
        context.quadraticCurveTo(spineX, childY, spineX + direction * radius, childY);
        context.lineTo(endX, childY);
    }

    function drawConnectors(context, from, children, side, scale, lineMode) {
        if (!children.length) {
            return;
        }

        var spineX = getBraceSpineX(from, children, side, scale);

        context.beginPath();

        children.forEach(function (child) {
            if (lineMode === "straight") {
                drawStraightConnector(context, from, child, side);
            } else if (lineMode === "orthogonal") {
                drawOrthogonalConnector(context, from, child, side, spineX);
            } else if (lineMode === "weighted-bezier") {
                drawWeightedBezierConnector(context, from, child, side);
            } else {
                drawRoundedOrthogonalConnector(context, from, child, side, scale, spineX);
            }
        });

        context.stroke();
    }

    function drawLines(map) {
        var viewport = getViewport(map);
        var canvas = map.querySelector("[data-gdf-mind-map-lines=\"true\"]");
        var rootTopic = map.querySelector(".gdf-mind-map-center [data-gdf-mind-map-topic=\"true\"]");

        if (!viewport || !canvas || !rootTopic || !isVisible(rootTopic)) {
            return;
        }

        var viewportRect = viewport.getBoundingClientRect();
        var ratio = resizeCanvas(canvas, viewportRect);
        var context = canvas.getContext("2d");
        var scale = getState(map).scale;
        var lineMode = getLineMode(map);

        context.setTransform(ratio, 0, 0, ratio, 0, 0);
        context.clearRect(0, 0, viewportRect.width, viewportRect.height);
        context.strokeStyle = getLineColor(canvas);
        context.lineWidth = clamp(1.05 * scale, 1, 2.4);
        context.lineCap = "round";
        context.lineJoin = "round";

        var rootCenter = getCenter(rootTopic, viewportRect);

        ["left", "right"].forEach(function (side) {
            var selector = ".gdf-mind-map-branches-" + side + " > [data-gdf-mind-map-branch=\"true\"] > [data-gdf-mind-map-topic=\"true\"]";
            var childCenters = [];

            map.querySelectorAll(selector).forEach(function (topic) {
                if (isVisible(topic)) {
                    childCenters.push(getCenter(topic, viewportRect));
                }
            });

            drawConnectors(context, rootCenter, childCenters, side, scale, lineMode);
        });

        map.querySelectorAll("[data-gdf-mind-map-branch=\"true\"]").forEach(function (branch) {
            var parentTopic = branch.querySelector(":scope > [data-gdf-mind-map-topic=\"true\"]");
            var children = branch.querySelector(":scope > [data-gdf-mind-map-children=\"true\"]");

            if (!parentTopic || !children || children.hidden || !isVisible(parentTopic)) {
                return;
            }

            var side = branch.getAttribute("data-gdf-mind-map-side") || "right";
            var parentCenter = getCenter(parentTopic, viewportRect);
            var childCenters = [];

            children.querySelectorAll(":scope > [data-gdf-mind-map-branch=\"true\"] > [data-gdf-mind-map-topic=\"true\"]").forEach(function (childTopic) {
                if (isVisible(childTopic)) {
                    childCenters.push(getCenter(childTopic, viewportRect));
                }
            });

            drawConnectors(context, parentCenter, childCenters, side, scale, lineMode);
        });
    }

    function scheduleDraw(map) {
        if (map._gdfMindMapDrawFrame) {
            return;
        }

        map._gdfMindMapDrawFrame = window.requestAnimationFrame(function () {
            map._gdfMindMapDrawFrame = null;
            drawLines(map);
        });
    }

    function animateLines(map, duration) {
        map._gdfMindMapDrawUntil = window.performance.now() + duration;

        if (map._gdfMindMapAnimationFrame) {
            return;
        }

        function tick() {
            drawLines(map);

            if (window.performance.now() < map._gdfMindMapDrawUntil) {
                map._gdfMindMapAnimationFrame = window.requestAnimationFrame(tick);
            } else {
                map._gdfMindMapAnimationFrame = null;
                scheduleDraw(map);
            }
        }

        map._gdfMindMapAnimationFrame = window.requestAnimationFrame(tick);
    }

    function setBranchState(button, expanded) {
        var branch = button.closest("[data-gdf-mind-map-branch=\"true\"]");
        var children = branch ? branch.querySelector(":scope > [data-gdf-mind-map-children=\"true\"]") : null;

        button.setAttribute("aria-expanded", expanded ? "true" : "false");

        if (branch) {
            branch.setAttribute("aria-expanded", expanded ? "true" : "false");
        }

        if (children) {
            children.hidden = !expanded;
        }
    }

    function initializeToolbar(map) {
        map.querySelectorAll("[data-gdf-mind-map-action]").forEach(function (button) {
            button.addEventListener("click", function () {
                var action = button.getAttribute("data-gdf-mind-map-action");
                var state = getState(map);

                if (action === "zoom-in") {
                    zoomAt(map, state.scale * 1.16);
                } else if (action === "zoom-out") {
                    zoomAt(map, state.scale / 1.16);
                } else if (action === "fit") {
                    fitView(map);
                } else if (action === "reset") {
                    resetView(map);
                }
            });
        });
    }

    function initializePanning(map) {
        var viewport = getViewport(map);

        if (!viewport) {
            return;
        }

        viewport.addEventListener("wheel", function (event) {
            event.preventDefault();
            var state = getState(map);
            var direction = event.deltaY < 0 ? 1.08 : 1 / 1.08;

            zoomAt(map, state.scale * direction, { x: event.clientX, y: event.clientY });
        }, { passive: false });

        viewport.addEventListener("pointerdown", function (event) {
            if (event.target.closest("button, [data-gdf-mind-map-toolbar=\"true\"]")) {
                return;
            }

            var state = getState(map);
            state.isDragging = true;
            state.dragStartX = event.clientX;
            state.dragStartY = event.clientY;
            state.originX = state.x;
            state.originY = state.y;
            map.classList.add("is-panning");
            viewport.setPointerCapture(event.pointerId);
        });

        viewport.addEventListener("pointermove", function (event) {
            var state = getState(map);

            if (!state.isDragging) {
                return;
            }

            state.x = state.originX + event.clientX - state.dragStartX;
            state.y = state.originY + event.clientY - state.dragStartY;
            applyTransform(map);
        });

        viewport.addEventListener("pointerup", function (event) {
            getState(map).isDragging = false;
            map.classList.remove("is-panning");

            if (viewport.hasPointerCapture(event.pointerId)) {
                viewport.releasePointerCapture(event.pointerId);
            }
        });

        viewport.addEventListener("pointercancel", function () {
            getState(map).isDragging = false;
            map.classList.remove("is-panning");
        });
    }

    function initializeMap(map) {
        if (map.dataset.gdfMindMapInitialized === "true") {
            scheduleDraw(map);
            return;
        }

        map.dataset.gdfMindMapInitialized = "true";

        map.querySelectorAll("[data-gdf-mind-map-toggle=\"true\"]").forEach(function (button) {
            setBranchState(button, button.getAttribute("aria-expanded") === "true");

            button.addEventListener("click", function () {
                setBranchState(button, button.getAttribute("aria-expanded") !== "true");
                scheduleDraw(map);
            });
        });

        initializeToolbar(map);
        initializePanning(map);

        var viewport = getViewport(map);

        if (viewport && (viewport.getAttribute("data-gdf-mind-map-auto-fit") || "true") === "true") {
            window.requestAnimationFrame(function () {
                fitView(map);
            });
        } else {
            resetView(map);
        }

        if ("ResizeObserver" in window) {
            var observer = new ResizeObserver(function () {
                scheduleDraw(map);
            });

            observer.observe(map);
        }

        window.addEventListener("resize", function () {
            scheduleDraw(map);
        });
    }

    function initialize() {
        document.querySelectorAll("[data-mind-map=\"true\"]").forEach(initializeMap);
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", initialize);
    } else {
        initialize();
    }
})();
