#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBComponentBuilder;
using DMBPageBuilder;
using NUnit.Framework;

#endregion

namespace DMBComponentBuilderUnitTest;

[TestFixture]
public sealed class EffectComposerTests
{
    [Test]
    public void EffectComposerCloneKeepsIndependentState()
    {
        TimelineEffectComposer composer = new TimelineEffectComposer()
            .SetOpacity()
            .SetSlide(TimelineSlideEffect.Left);
        IIsCssClassComposer clone = composer.Clone();

        composer.SetScale().SetSlide(TimelineSlideEffect.Right);

        Assert.Multiple(() =>
        {
            Assert.That(clone.BuildClasses(), Is.EqualTo(new[] { "timeline-effect-opacity", "timeline-effect-slide-left" }));
            Assert.That(composer.BuildClasses(), Is.EqualTo(new[] { "timeline-effect-opacity", "timeline-effect-scale", "timeline-effect-slide-right" }));
        });
    }

    [Test]
    public void RoadmapEffectComposerBuildsConfiguredClasses()
    {
        RoadmapEffectComposer composer = new RoadmapEffectComposer()
            .SetPlot()
            .SetHoverScale()
            .SetSlide(RoadmapSlideEffect.Bottom);

        Assert.That(composer.BuildClasses(), Is.EqualTo(new[]
        {
            "roadmap-effect-plot",
            "roadmap-effect-hover-scale",
            "roadmap-effect-slide-bottom"
        }));
    }

    [Test]
    public void TimelineEffectComposerBuildsConfiguredClasses()
    {
        TimelineEffectComposer composer = new TimelineEffectComposer()
            .SetOpacity()
            .SetScale()
            .SetHoverLift()
            .SetSlide(TimelineSlideEffect.Alternate);

        Assert.That(composer.BuildClasses(), Is.EqualTo(new[]
        {
            "timeline-effect-opacity",
            "timeline-effect-scale",
            "timeline-effect-hover-lift",
            "timeline-effect-slide-alternate"
        }));
    }
}