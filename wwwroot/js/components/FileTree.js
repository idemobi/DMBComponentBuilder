(function () {
    "use strict";

    function getDirectChildByClass(element, className) {
        if (!element) {
            return null;
        }

        for (var index = 0; index < element.children.length; index++) {
            if (element.children[index].classList.contains(className)) {
                return element.children[index];
            }
        }

        return null;
    }

    function setFolderState(button, expanded) {
        var item = button.closest(".file-tree-item");
        var row = getDirectChildByClass(item, "file-tree-row");
        var children = getDirectChildByClass(item, "file-tree-children");
        var icon = row ? row.querySelector(".file-tree-icon") : null;

        button.setAttribute("aria-expanded", expanded ? "true" : "false");

        if (item) {
            item.setAttribute("aria-expanded", expanded ? "true" : "false");
        }

        if (children) {
            children.hidden = !expanded;
        }

        if (icon && item && item.getAttribute("data-file-tree-kind") === "folder") {
            icon.classList.toggle("bi-folder2-open", expanded);
            icon.classList.toggle("bi-folder2", !expanded);
        }
    }

    function initializeTree(tree) {
        var buttons = tree.querySelectorAll("[data-file-tree-toggle=\"true\"]");

        buttons.forEach(function (button) {
            if (button.dataset.fileTreeInitialized === "true") {
                return;
            }

            button.dataset.fileTreeInitialized = "true";
            setFolderState(button, button.getAttribute("aria-expanded") === "true");

            button.addEventListener("click", function () {
                setFolderState(button, button.getAttribute("aria-expanded") !== "true");
            });
        });
    }

    function initialize() {
        document.querySelectorAll("[data-file-tree=\"true\"]").forEach(initializeTree);
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", initialize);
    } else {
        initialize();
    }
})();
