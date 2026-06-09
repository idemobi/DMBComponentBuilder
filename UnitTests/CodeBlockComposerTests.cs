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
public sealed class CodeBlockComposerTests
{
    [Test]
    public void BuildClassesIncludesThemeAndEnabledOptions()
    {
        CodeBlockComposer composer = new CodeBlockComposer()
            .SetTheme(CodeBlockTheme.Dark)
            .SetCompact()
            .SetHighlightDisabled();

        IReadOnlyList<string> classes = composer.BuildClasses();

        Assert.That(classes, Is.EqualTo(new[]
        {
            "dmb-code-block-dark",
            "dmb-code-block-compact",
            "dmb-code-block-highlight-disabled"
        }));
    }

    [Test]
    public void CloneKeepsIndependentCodeBlockComposerState()
    {
        CodeBlockComposer composer = new CodeBlockComposer()
            .SetTheme(CodeBlockTheme.Light)
            .SetCompact();
        IIsCssClassComposer clone = composer.Clone();

        composer.SetTheme(CodeBlockTheme.Dark).SetHighlightDisabled();

        Assert.Multiple(() =>
        {
            Assert.That(clone.BuildClasses(), Is.EqualTo(new[] { "dmb-code-block-light", "dmb-code-block-compact" }));
            Assert.That(composer.BuildClasses(), Is.EqualTo(new[] { "dmb-code-block-dark", "dmb-code-block-compact", "dmb-code-block-highlight-disabled" }));
        });
    }
}