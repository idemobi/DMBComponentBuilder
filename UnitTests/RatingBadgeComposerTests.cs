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
public sealed class RatingBadgeComposerTests
{
    [Test]
    public void BuildClassesIncludesVariantCartoucheAndSize()
    {
        RatingBadgeComposer composer = new RatingBadgeComposer()
            .SetVariant(VariantStyle.Warning)
            .SetCartouche()
            .SetSize(RatingBadgeSize.Large);

        Assert.That(composer.BuildClasses(), Is.EqualTo(new[]
        {
            "text-warning",
            "rating-badge-cartouche",
            "rating-badge-cartouche-warning",
            "rating-badge-lg"
        }));
    }

    [Test]
    public void CloneKeepsIndependentRatingBadgeComposerState()
    {
        RatingBadgeComposer composer = new RatingBadgeComposer()
            .SetVariant(VariantStyle.Success)
            .SetSize(RatingBadgeSize.Small);
        IIsCssClassComposer clone = composer.Clone();

        composer.SetVariant(VariantStyle.Danger).SetCartouche();

        Assert.Multiple(() =>
        {
            Assert.That(clone.BuildClasses(), Is.EqualTo(new[] { "text-success", "rating-badge-sm" }));
            Assert.That(composer.BuildClasses(), Is.EqualTo(new[] { "text-danger", "rating-badge-cartouche", "rating-badge-cartouche-danger", "rating-badge-sm" }));
        });
    }
}