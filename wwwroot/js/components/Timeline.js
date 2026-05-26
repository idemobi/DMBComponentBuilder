(function () {
    function initTimelineEffects() {
        const rows = document.querySelectorAll(
            ".timeline-effect-opacity .timeline-row," +
            ".timeline-effect-slide-left .timeline-row," +
            ".timeline-effect-slide-right .timeline-row," +
            ".timeline-effect-slide-alternate .timeline-row," +
            ".timeline-effect-scale .timeline-row"
        );

        if (!rows.length) {
            return;
        }

        if (!("IntersectionObserver" in window)) {
            rows.forEach(row => row.classList.add("timeline-row-visible"));
            return;
        }

        const observer = new IntersectionObserver(entries => {
            entries.forEach(entry => {
                if (!entry.isIntersecting) {
                    return;
                }

                entry.target.classList.add("timeline-row-visible");
                observer.unobserve(entry.target);
            });
        }, {
            threshold: 0.2,
            rootMargin: "0px 0px -10% 0px"
        });

        rows.forEach(row => observer.observe(row));
    }

    document.addEventListener("DOMContentLoaded", initTimelineEffects);
})();