#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBComponentBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using NUnit.Framework;

#endregion

namespace DMBComponentBuilderUnitTest;

[TestFixture]
public sealed class ConversationMessageTests
{
    [Test]
    public void CloneCopiesConversationMessageValues()
    {
        HtmlString accessoryContent = new("<button>Open</button>");
        HtmlString htmlContent = new("<strong>Hello</strong>");
        DateTimeOffset createdAt = new(2026, 6, 18, 8, 30, 0, TimeSpan.Zero);
        ConversationMessage message = new ConversationMessage
        {
            AccessoryContent = accessoryContent,
            AuthorName = "Customer",
            AuthorSubtitle = "Organization owner",
            AvatarBadgeText = "admin",
            AvatarBadgeVariant = VariantStyle.Danger,
            BubbleVariant = VariantStyle.Success,
            CreatedAt = createdAt,
            DateText = "Today",
            HtmlContent = htmlContent,
            Icon = IconStruct.Bootstrap("bi-person-fill"),
            IsCurrentParticipant = true,
            ParticipantKey = "customer-1",
            Text = "Hello",
            Variant = VariantStyle.Warning
        };

        ConversationMessage clone = message.Clone();

        Assert.Multiple(() =>
        {
            Assert.That(clone, Is.Not.SameAs(message));
            Assert.That(clone.AccessoryContent, Is.SameAs(accessoryContent));
            Assert.That(clone.AuthorName, Is.EqualTo("Customer"));
            Assert.That(clone.AuthorSubtitle, Is.EqualTo("Organization owner"));
            Assert.That(clone.AvatarBadgeText, Is.EqualTo("admin"));
            Assert.That(clone.AvatarBadgeVariant, Is.EqualTo(VariantStyle.Danger));
            Assert.That(clone.BubbleVariant, Is.EqualTo(VariantStyle.Success));
            Assert.That(clone.CreatedAt, Is.EqualTo(createdAt));
            Assert.That(clone.DateText, Is.EqualTo("Today"));
            Assert.That(clone.HtmlContent, Is.SameAs(htmlContent));
            Assert.That(clone.IsCurrentParticipant, Is.True);
            Assert.That(clone.ParticipantKey, Is.EqualTo("customer-1"));
            Assert.That(clone.Text, Is.EqualTo("Hello"));
            Assert.That(clone.Variant, Is.EqualTo(VariantStyle.Warning));
        });
    }

    [Test]
    public void IsOwnedByUsesExplicitCurrentParticipantFlagFirst()
    {
        ConversationMessage message = new ConversationMessage
        {
            ParticipantKey = "other",
            IsCurrentParticipant = true
        };

        Assert.That(message.IsOwnedBy("current"), Is.True);
    }

    [Test]
    public void IsOwnedByUsesParticipantKeyWhenFlagIsNotSet()
    {
        ConversationMessage message = new ConversationMessage
        {
            ParticipantKey = "Customer-1"
        };

        Assert.Multiple(() =>
        {
            Assert.That(message.IsOwnedBy("customer-1"), Is.True);
            Assert.That(message.IsOwnedBy("admin-1"), Is.False);
            Assert.That(message.IsOwnedBy(null), Is.False);
        });
    }

    [Test]
    public void ResolveDateTextPrefersExplicitText()
    {
        ConversationMessage message = new ConversationMessage
        {
            DateText = "Yesterday",
            CreatedAt = DateTimeOffset.UtcNow
        };

        Assert.That(message.ResolveDateText(), Is.EqualTo("Yesterday"));
    }

    [Test]
    public void ResolveDateTextReturnsEmptyWhenNoDateIsConfigured()
    {
        ConversationMessage message = new ConversationMessage();

        Assert.That(message.ResolveDateText(), Is.Empty);
    }
}
