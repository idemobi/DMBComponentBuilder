using DMBPageBuilder;

namespace DMBComponentBuilder
{
    public sealed class TimelineEffectComposer : IIsCssClassComposer
    {
        private bool _opacity;
        private bool _scale;
        private bool _plot;
        private bool _hoverScale;
        private bool _hoverLift;
        private TimelineSlideEffect _slide = TimelineSlideEffect.None;

        public TimelineEffectComposer SetOpacity(bool value = true)
        {
            _opacity = value;
            return this;
        }

        public TimelineEffectComposer SetScale(bool value = true)
        {
            _scale = value;
            return this;
        }

        public TimelineEffectComposer SetPlot(bool value = true)
        {
            _plot = value;
            return this;
        }

        public TimelineEffectComposer SetHoverScale(bool value = true)
        {
            _hoverScale = value;
            return this;
        }

        public TimelineEffectComposer SetHoverLift(bool value = true)
        {
            _hoverLift = value;
            return this;
        }

        public TimelineEffectComposer SetSlide(TimelineSlideEffect effect)
        {
            _slide = effect;
            return this;
        }

        public IReadOnlyList<string> BuildClasses()
        {
            List<string> classes = new();

            if (_opacity) classes.Add("timeline-effect-opacity");
            if (_scale) classes.Add("timeline-effect-scale");
            if (_plot) classes.Add("timeline-effect-plot");
            if (_hoverScale) classes.Add("timeline-effect-hover-scale");
            if (_hoverLift) classes.Add("timeline-effect-hover-lift");

            switch (_slide)
            {
                case TimelineSlideEffect.Left:
                    classes.Add("timeline-effect-slide-left");
                    break;

                case TimelineSlideEffect.Right:
                    classes.Add("timeline-effect-slide-right");
                    break;

                case TimelineSlideEffect.Alternate:
                    classes.Add("timeline-effect-slide-alternate");
                    break;
            }

            return classes;
        }

        public IIsCssClassComposer Clone()
        {
            return new TimelineEffectComposer()
                .SetOpacity(_opacity)
                .SetScale(_scale)
                .SetPlot(_plot)
                .SetHoverScale(_hoverScale)
                .SetHoverLift(_hoverLift)
                .SetSlide(_slide);
        }
    }
}