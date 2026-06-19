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
public sealed class AvatarBadgeTests
{
    [Test]
    public void CloneCopiesAvatarBadgeValues()
    {
        AvatarBadge badge = new()
        {
            Text = "admin",
            Variant = VariantStyle.Danger
        };

        AvatarBadge clone = badge.Clone();

        Assert.Multiple(() =>
        {
            Assert.That(clone, Is.Not.SameAs(badge));
            Assert.That(clone.Text, Is.EqualTo("admin"));
            Assert.That(clone.Variant, Is.EqualTo(VariantStyle.Danger));
        });
    }
}
