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
public sealed class CodeLanguageExtensionTests
{
    [Test]
    public void GetBootstrapIconClassReturnsExpectedClasses()
    {
        Assert.Multiple(() =>
        {
            Assert.That(CodeLanguage.CSharp.GetBootstrapIconClass(), Is.EqualTo("bi-filetype-cs"));
            Assert.That(CodeLanguage.Markdown.GetBootstrapIconClass(), Is.EqualTo("bi-filetype-md"));
            Assert.That(CodeLanguage.PlainText.GetBootstrapIconClass(), Is.EqualTo("bi-file-earmark-text"));
        });
    }

    [Test]
    public void GetCodeBlockLanguageReturnsExpectedTokens()
    {
        Assert.Multiple(() =>
        {
            Assert.That(CodeLanguage.CSharp.GetCodeBlockLanguage(), Is.EqualTo("csharp"));
            Assert.That(CodeLanguage.Html.GetCodeBlockLanguage(), Is.EqualTo("markup"));
            Assert.That(CodeLanguage.JavaScript.GetCodeBlockLanguage(), Is.EqualTo("javascript"));
            Assert.That(CodeLanguage.PlainText.GetCodeBlockLanguage(), Is.EqualTo("none"));
        });
    }

    [Test]
    public void GetDisplayNameReturnsExpectedLabels()
    {
        Assert.Multiple(() =>
        {
            Assert.That(CodeLanguage.CSharp.GetDisplayName(), Is.EqualTo("C#"));
            Assert.That(CodeLanguage.Json.GetDisplayName(), Is.EqualTo("JSON"));
            Assert.That(CodeLanguage.PlainText.GetDisplayName(), Is.EqualTo("Text"));
            Assert.That(CodeLanguage.TypeScript.GetDisplayName(), Is.EqualTo("TypeScript"));
        });
    }
}