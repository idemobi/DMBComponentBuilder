(function () {
    function resolveAvatar(target) {
        if (!target) {
            return null;
        }

        return document.querySelector('[data-avatar-preview="' + target + '"]');
    }

    function setIcon(avatar, iconClass) {
        if (!avatar || !iconClass) {
            return;
        }

        var surface = avatar.querySelector(".avatar-builder-surface");
        if (!surface) {
            return;
        }

        surface.innerHTML = "";

        var icon = document.createElement("i");
        icon.className = iconClass.indexOf("bi ") === 0 ? iconClass : "bi " + iconClass;
        icon.classList.add("avatar-builder-icon");
        surface.appendChild(icon);
    }

    function setName(avatar, name) {
        if (!avatar) {
            return;
        }

        avatar.setAttribute("aria-label", name || "Avatar");
        avatar.setAttribute("data-avatar-display-name", name || "");
    }

    function setColors(avatar, background, foreground) {
        if (!avatar) {
            return;
        }

        if (background) {
            avatar.style.setProperty("--avatar-bg", background);
        }

        if (foreground) {
            avatar.style.setProperty("--avatar-fg", foreground);
        }
    }

    function bindIconInput(input) {
        var target = input.getAttribute("data-avatar-preview-icon");
        var avatar = resolveAvatar(target);

        if (!avatar) {
            return;
        }

        input.addEventListener("change", function () {
            if (input.checked) {
                setIcon(avatar, input.value);
            }
        });
    }

    function bindColorInput(input) {
        var target = input.getAttribute("data-avatar-preview-color");
        var avatar = resolveAvatar(target);

        if (!avatar) {
            return;
        }

        input.addEventListener("change", function () {
            if (input.checked) {
                setColors(
                    avatar,
                    input.getAttribute("data-avatar-preview-background"),
                    input.getAttribute("data-avatar-preview-foreground"));
            }
        });
    }

    function bindNameInput(input) {
        var target = input.getAttribute("data-avatar-preview-name");
        var avatar = resolveAvatar(target);
        var previewName = document.querySelector('[data-avatar-preview-name-output="' + target + '"]');

        if (!avatar && !previewName) {
            return;
        }

        input.addEventListener("input", function () {
            var value = (input.value || "").trim();
            var fallback = input.defaultValue || "";
            var resolved = value || fallback;

            setName(avatar, resolved);

            if (previewName) {
                previewName.textContent = resolved;
            }
        });
    }

    function bindImageFallback(image) {
        function markImageError() {
            var avatar = image.closest("[data-avatar-builder]");

            if (avatar) {
                avatar.classList.add("avatar-builder-image-error");
            }
        }

        image.addEventListener("error", function () {
            markImageError();
        });

        if (image.complete && image.naturalWidth === 0) {
            markImageError();
        }
    }

    function bindPreviewInputs(root) {
        var scope = root || document;
        var iconInputs = scope.querySelectorAll("[data-avatar-preview-icon]");
        var colorInputs = scope.querySelectorAll("[data-avatar-preview-color]");
        var nameInputs = scope.querySelectorAll("[data-avatar-preview-name]");
        var images = scope.querySelectorAll("[data-avatar-image]");
        var index;

        for (index = 0; index < iconInputs.length; index++) {
            bindIconInput(iconInputs[index]);
        }

        for (index = 0; index < colorInputs.length; index++) {
            bindColorInput(colorInputs[index]);
        }

        for (index = 0; index < nameInputs.length; index++) {
            bindNameInput(nameInputs[index]);
        }

        for (index = 0; index < images.length; index++) {
            bindImageFallback(images[index]);
        }
    }

    window.DMBAvatarBuilder = {
        bindPreviewInputs: bindPreviewInputs,
        setColors: setColors,
        setIcon: setIcon,
        setName: setName
    };

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", function () {
            bindPreviewInputs(document);
        });
    } else {
        bindPreviewInputs(document);
    }
})();
