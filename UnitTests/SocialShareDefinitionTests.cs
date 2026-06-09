#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBComponentBuilder;
using NUnit.Framework;

#endregion

namespace DMBComponentBuilderUnitTest;

[TestFixture]
public sealed class SocialShareDefinitionTests
{
    [Test]
    public void AddRemoveAndLabelsUpdateShareDefinition()
    {
        SocialShareDefinition definition = new SocialShareDefinition()
            .SetPlatforms(SocialSharePlatform.Email)
            .AddPlatform(SocialSharePlatform.Discord)
            .AddPlatform(SocialSharePlatform.Discord)
            .RemovePlatform(SocialSharePlatform.Email)
            .SetLabel(SocialSharePlatform.Discord, "Send to Discord");

        Assert.Multiple(() =>
        {
            Assert.That(definition.Platforms, Is.EqualTo(new[] { SocialSharePlatform.Discord }));
            Assert.That(definition.Labels[SocialSharePlatform.Discord], Is.EqualTo("Send to Discord"));
        });
    }

    [Test]
    public void ConstructorUsesDefaultPlatforms()
    {
        SocialShareDefinition definition = new SocialShareDefinition("Title", "https://example.com");

        Assert.Multiple(() =>
        {
            Assert.That(definition.Title, Is.EqualTo("Title"));
            Assert.That(definition.Url, Is.EqualTo("https://example.com"));
            Assert.That(definition.Platforms, Is.EqualTo(new[]
            {
                SocialSharePlatform.Email,
                SocialSharePlatform.Facebook,
                SocialSharePlatform.WhatsApp,
                SocialSharePlatform.Reddit,
                SocialSharePlatform.Twitter,
                SocialSharePlatform.LinkedIn,
                SocialSharePlatform.Discord
            }));
        });
    }

    [Test]
    public void SetPlatformsDeduplicatesAndReplacesPlatforms()
    {
        SocialShareDefinition definition = new SocialShareDefinition()
            .SetPlatforms(SocialSharePlatform.Email, SocialSharePlatform.Email, SocialSharePlatform.Reddit);

        Assert.That(definition.Platforms, Is.EqualTo(new[] { SocialSharePlatform.Email, SocialSharePlatform.Reddit }));
    }

    [Test]
    public void UseAllPlatformsIncludesEveryEnumValue()
    {
        SocialShareDefinition definition = new SocialShareDefinition().UseAllPlatforms();

        Assert.That(definition.Platforms, Is.EqualTo(Enum.GetValues<SocialSharePlatform>()));
    }
}