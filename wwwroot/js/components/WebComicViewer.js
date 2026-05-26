(function () {
    function setMode(viewer, mode) {
        viewer.classList.remove("web-comic-mode-fitwidth", "web-comic-mode-fitheight", "web-comic-mode-fullimage");
        viewer.classList.add("web-comic-mode-" + mode);

        viewer.querySelectorAll(".web-comic-tool").forEach(function (button) {
            button.classList.remove("web-comic-tool-active");
        });

        var toolClass = mode === "fitwidth" ? "fit-width" : mode === "fitheight" ? "fit-height" : "full-image";
        var active = viewer.querySelector(".web-comic-" + toolClass);
        if (active) {
            active.classList.add("web-comic-tool-active");
        }
    }

    function updateCheck(viewer, key, state, text) {
        var check = viewer.querySelector('[data-web-comic-check="' + key + '"]');
        if (!check) {
            return;
        }

        check.classList.remove("web-comic-check-ok", "web-comic-check-warning", "web-comic-check-error");
        check.classList.add("web-comic-check-" + state);

        var value = check.querySelector("dd");
        if (value) {
            value.textContent = text;
        }
    }

    function inspectImage(viewer, image) {
        if (!image || !image.complete || !image.naturalWidth) {
            return;
        }

        var width = image.naturalWidth;
        var height = image.naturalHeight;
        var ratio = width / height;
        var minWidth = Number(viewer.getAttribute("data-web-comic-min-width") || 1080);
        var minHeight = Number(viewer.getAttribute("data-web-comic-min-height") || 1080);
        var sizeOk = width >= minWidth && height >= minHeight;
        var ratioText = ratio.toFixed(2) + ":1";
        var isVeryTall = ratio < 0.62;
        var isVeryWide = ratio > 2.2;

        updateCheck(viewer, "image", "ok", "Loaded: " + width + " x " + height + "px");
        updateCheck(
            viewer,
            "dimensions",
            sizeOk ? "ok" : "warning",
            sizeOk ? "Enough resolution for social previews" : "Below " + minWidth + " x " + minHeight + "px"
        );
        updateCheck(
            viewer,
            "ratio",
            isVeryTall || isVeryWide ? "warning" : "ok",
            isVeryTall ? "Very tall image: check feed crops" : isVeryWide ? "Very wide image: check mobile readability" : "Balanced ratio " + ratioText
        );
        updateCheck(viewer, "reader", "ok", "Reader and preview images are available");
    }

    function inspectRequiredImages(viewer) {
        var requiredImages = Array.prototype.slice.call(viewer.querySelectorAll("[data-web-comic-required]"));
        var failed = requiredImages.filter(function (image) {
            return image.getAttribute("data-web-comic-load-error") === "true";
        });
        var pending = requiredImages.filter(function (image) {
            return !image.complete;
        });

        if (failed.length > 0) {
            updateCheck(viewer, "reader", "error", failed.length + " required image(s) missing");
            return;
        }

        if (pending.length === 0 && requiredImages.length > 0) {
            updateCheck(viewer, "reader", "ok", requiredImages.length + " required image(s) loaded");
        }
    }

    function bindViewer(viewer) {
        viewer.querySelectorAll(".web-comic-fit-width").forEach(function (button) {
            button.addEventListener("click", function () {
                setMode(viewer, "fitwidth");
            });
        });

        viewer.querySelectorAll(".web-comic-fit-height").forEach(function (button) {
            button.addEventListener("click", function () {
                setMode(viewer, "fitheight");
            });
        });

        viewer.querySelectorAll(".web-comic-full-image").forEach(function (button) {
            button.addEventListener("click", function () {
                setMode(viewer, "fullimage");
            });
        });

        viewer.querySelectorAll("[data-web-comic-image]").forEach(function (image) {
            image.addEventListener("load", function () {
                inspectRequiredImages(viewer);
            });

            image.addEventListener("error", function () {
                image.setAttribute("data-web-comic-load-error", "true");
                inspectRequiredImages(viewer);
            });
        });

        var readerImage = viewer.querySelector('[data-web-comic-image="reader"]') || viewer.querySelector("[data-web-comic-image]");
        if (readerImage) {
            if (readerImage.complete) {
                inspectImage(viewer, readerImage);
                inspectRequiredImages(viewer);
            }

            readerImage.addEventListener("load", function () {
                inspectImage(viewer, readerImage);
                inspectRequiredImages(viewer);
            });

            readerImage.addEventListener("error", function () {
                readerImage.setAttribute("data-web-comic-load-error", "true");
                updateCheck(viewer, "image", "error", "Image could not be loaded");
                updateCheck(viewer, "reader", "error", "Reader cannot verify a missing image");
            });
        }
    }

    function init() {
        document.querySelectorAll("[data-web-comic-viewer]").forEach(bindViewer);
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", init);
    } else {
        init();
    }
})();
