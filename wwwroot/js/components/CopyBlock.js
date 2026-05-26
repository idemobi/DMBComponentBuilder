(function () {
    "use strict";

    function setCopiedState(button) {
        var style = button.getAttribute("data-copy-block-style") || "btn btn-sm btn-outline-primary";
        button.className = style;
        button.innerHTML = '<span class="bi-clipboard-check" aria-hidden="true"></span><span class="visually-hidden">Copied</span>';
    }

    function copyFromTarget(button) {
        var targetId = button.getAttribute("data-copy-block-target");
        if (!targetId || !navigator.clipboard || !navigator.clipboard.writeText) {
            return;
        }

        var target = document.getElementById(targetId);
        if (!target) {
            return;
        }

        var content = (target.textContent || "").trim();
        navigator.clipboard.writeText(content).then(function () {
            setCopiedState(button);
        });
    }

    document.addEventListener("click", function (event) {
        var target = event.target;
        var button = target && target.closest
            ? target.closest("[data-copy-block-button='true']")
            : null;

        if (!button) {
            return;
        }

        event.preventDefault();
        copyFromTarget(button);
    });
}());
