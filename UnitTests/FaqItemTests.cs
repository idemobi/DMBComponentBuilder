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
public sealed class FaqItemTests
{
    [Test]
    public void FaqItemStoresQuestionAnswerIconsAndOptionalContent()
    {
        FaqItem item = new FaqItem
        {
            Question = "Can I test this?",
            Answer = "Yes.",
            IsVisible = false,
            ActionContent = new HtmlString("<button>Open</button>"),
            HiddenNotice = new HtmlString("<span>Hidden</span>")
        };

        item.StartIcons.Add(IconStruct.Bootstrap("bi-question-circle"));
        item.EndIcons.Add(IconStruct.Bootstrap("bi-chevron-down"));

        Assert.Multiple(() =>
        {
            Assert.That(item.Question, Is.EqualTo("Can I test this?"));
            Assert.That(item.Answer, Is.EqualTo("Yes."));
            Assert.That(item.IsVisible, Is.False);
            Assert.That(item.StartIcons, Has.Count.EqualTo(1));
            Assert.That(item.EndIcons, Has.Count.EqualTo(1));
            Assert.That(item.ActionContent, Is.Not.Null);
            Assert.That(item.HiddenNotice, Is.Not.Null);
        });
    }
}