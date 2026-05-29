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
    ///     Represents timeline effect composer data used by timeline components.
    /// </summary>
    public sealed class TimelineEffectComposer : IIsCssClassComposer
    {
        #region Instance fields and properties

        private bool _hoverLift;
        private bool _hoverScale;
        private bool _opacity;
        private bool _plot;
        private bool _scale;
        private TimelineSlideEffect _slide = TimelineSlideEffect.None;

        #endregion

        #region Instance methods

        /// <summary>
        ///     Configures the hover lift for the timeline component.
        /// </summary>
        /// <param name="value">True to enable the option; false to disable it.</param>
        /// <returns>The generated timeline value.</returns>
        public TimelineEffectComposer SetHoverLift(bool value = true)
        {
            _hoverLift = value;
            return this;
        }

        /// <summary>
        ///     Configures the hover scale for the timeline component.
        /// </summary>
        /// <param name="value">True to enable the option; false to disable it.</param>
        /// <returns>The generated timeline value.</returns>
        public TimelineEffectComposer SetHoverScale(bool value = true)
        {
            _hoverScale = value;
            return this;
        }

        /// <summary>
        ///     Configures the opacity for the timeline component.
        /// </summary>
        /// <param name="value">True to enable the option; false to disable it.</param>
        /// <returns>The generated timeline value.</returns>
        public TimelineEffectComposer SetOpacity(bool value = true)
        {
            _opacity = value;
            return this;
        }

        /// <summary>
        ///     Configures the plot for the timeline component.
        /// </summary>
        /// <param name="value">True to enable the option; false to disable it.</param>
        /// <returns>The generated timeline value.</returns>
        public TimelineEffectComposer SetPlot(bool value = true)
        {
            _plot = value;
            return this;
        }

        /// <summary>
        ///     Configures the scale for the timeline component.
        /// </summary>
        /// <param name="value">True to enable the option; false to disable it.</param>
        /// <returns>The generated timeline value.</returns>
        public TimelineEffectComposer SetScale(bool value = true)
        {
            _scale = value;
            return this;
        }

        /// <summary>
        ///     Configures the slide for the timeline component.
        /// </summary>
        /// <param name="effect">The effect value.</param>
        /// <returns>The generated timeline value.</returns>
        public TimelineEffectComposer SetSlide(TimelineSlideEffect effect)
        {
            _slide = effect;
            return this;
        }

        #region From interface IIsCssClassComposer

        /// <summary>
        ///     Builds the CSS classes configured for timeline rendering.
        /// </summary>
        /// <returns>The generated timeline value.</returns>
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

        /// <summary>
        ///     Creates a copy of the current timeline definition.
        /// </summary>
        /// <returns>The generated timeline value.</returns>
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

        #endregion

        #endregion
    }
}