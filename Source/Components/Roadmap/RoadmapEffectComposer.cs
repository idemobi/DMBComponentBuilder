#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBPageBuilder;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Represents roadmap effect composer data used by roadmap components.
    /// </summary>
    public sealed class RoadmapEffectComposer : IIsCssClassComposer
    {
        #region Instance fields and properties

        private bool _hoverLift;
        private bool _hoverScale;
        private bool _opacity;
        private bool _plot;
        private bool _scale;
        private RoadmapSlideEffect _slide = RoadmapSlideEffect.None;

        #endregion

        #region Instance methods

        /// <summary>
        ///     Configures the hover lift for the roadmap component.
        /// </summary>
        /// <param name="value">True to enable the option; false to disable it.</param>
        /// <returns>The generated roadmap value.</returns>
        public RoadmapEffectComposer SetHoverLift(bool value = true)
        {
            _hoverLift = value;
            return this;
        }

        /// <summary>
        ///     Configures the hover scale for the roadmap component.
        /// </summary>
        /// <param name="value">True to enable the option; false to disable it.</param>
        /// <returns>The generated roadmap value.</returns>
        public RoadmapEffectComposer SetHoverScale(bool value = true)
        {
            _hoverScale = value;
            return this;
        }

        /// <summary>
        ///     Configures the opacity for the roadmap component.
        /// </summary>
        /// <param name="value">True to enable the option; false to disable it.</param>
        /// <returns>The generated roadmap value.</returns>
        public RoadmapEffectComposer SetOpacity(bool value = true)
        {
            _opacity = value;
            return this;
        }

        /// <summary>
        ///     Configures the plot for the roadmap component.
        /// </summary>
        /// <param name="value">True to enable the option; false to disable it.</param>
        /// <returns>The generated roadmap value.</returns>
        public RoadmapEffectComposer SetPlot(bool value = true)
        {
            _plot = value;
            return this;
        }

        /// <summary>
        ///     Configures the scale for the roadmap component.
        /// </summary>
        /// <param name="value">True to enable the option; false to disable it.</param>
        /// <returns>The generated roadmap value.</returns>
        public RoadmapEffectComposer SetScale(bool value = true)
        {
            _scale = value;
            return this;
        }

        /// <summary>
        ///     Configures the slide for the roadmap component.
        /// </summary>
        /// <param name="effect">The effect value.</param>
        /// <returns>The generated roadmap value.</returns>
        public RoadmapEffectComposer SetSlide(RoadmapSlideEffect effect)
        {
            _slide = effect;
            return this;
        }

        #region From interface IIsCssClassComposer

        /// <summary>
        ///     Builds the CSS classes configured for roadmap rendering.
        /// </summary>
        /// <returns>The generated roadmap value.</returns>
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

        /// <summary>
        ///     Creates a copy of the current roadmap definition.
        /// </summary>
        /// <returns>The generated roadmap value.</returns>
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

        #endregion

        #endregion
    }
}