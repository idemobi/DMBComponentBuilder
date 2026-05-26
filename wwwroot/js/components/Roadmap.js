(function () {
    function initRoadmapEffects() {
        const items = document.querySelectorAll(
            ".roadmap-effect-opacity .roadmap-item," +
            ".roadmap-effect-slide-left .roadmap-item," +
            ".roadmap-effect-slide-right .roadmap-item," +
            ".roadmap-effect-slide-bottom .roadmap-item," +
            ".roadmap-effect-scale .roadmap-item"
        );

        if (!items.length) {
            return;
        }

        if (!("IntersectionObserver" in window)) {
            items.forEach(item => item.classList.add("roadmap-item-visible"));
            return;
        }

        const observer = new IntersectionObserver(entries => {
            entries.forEach(entry => {
                if (!entry.isIntersecting) {
                    return;
                }

                entry.target.classList.add("roadmap-item-visible");
                observer.unobserve(entry.target);
            });
        }, {
            threshold: 0.2,
            rootMargin: "0px 0px -10% 0px"
        });

        items.forEach(item => observer.observe(item));
    }

    document.addEventListener("DOMContentLoaded", initRoadmapEffects);
})();