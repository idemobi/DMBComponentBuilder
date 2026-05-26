using DMBPageBuilder;

namespace DMBComponentBuilder
{
    public sealed class RoadmapEffectComposer : IIsCssClassComposer
    {
        private bool _opacity;
        private bool _scale;
        private bool _plot;
        private bool _hoverScale;
        private bool _hoverLift;
        private RoadmapSlideEffect _slide = RoadmapSlideEffect.None;

        public RoadmapEffectComposer SetOpacity(bool value = true)
        {
            _opacity = value;
            return this;
        }

        public RoadmapEffectComposer SetScale(bool value = true)
        {
            _scale = value;
            return this;
        }

        public RoadmapEffectComposer SetPlot(bool value = true)
        {
            _plot = value;
            return this;
        }

        public RoadmapEffectComposer SetHoverScale(bool value = true)
        {
            _hoverScale = value;
            return this;
        }

        public RoadmapEffectComposer SetHoverLift(bool value = true)
        {
            _hoverLift = value;
            return this;
        }

        public RoadmapEffectComposer SetSlide(RoadmapSlideEffect effect)
        {
            _slide = effect;
            return this;
        }

        public IReadOnlyList<string> BuildClasses()
        {
            List<string> classes = new();

            if (_opacity)
            {
                classes.Add("roadmap-effect-opacity");
            }

            if (_scale)
            {
                classes.Add("roadmap-effect-scale");
            }

            if (_plot)
            {
                classes.Add("roadmap-effect-plot");
            }

            if (_hoverScale)
            {
                classes.Add("roadmap-effect-hover-scale");
            }

            if (_hoverLift)
            {
                classes.Add("roadmap-effect-hover-lift");
            }

            switch (_slide)
            {
                case RoadmapSlideEffect.Left:
                    classes.Add("roadmap-effect-slide-left");
                break;

                case RoadmapSlideEffect.Right:
                    classes.Add("roadmap-effect-slide-right");
                break;

                case RoadmapSlideEffect.Bottom:
                    classes.Add("roadmap-effect-slide-bottom");
                break;
            }

            return classes;
        }

        public IIsCssClassComposer Clone()
        {
            return new RoadmapEffectComposer()
                .SetOpacity(_opacity)
                .SetScale(_scale)
                .SetPlot(_plot)
                .SetHoverScale(_hoverScale)
                .SetHoverLift(_hoverLift)
                .SetSlide(_slide);
        }
    }
}