#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using DMBComponentBuilder;
using NUnit.Framework;

#endregion

namespace DMBComponentBuilderUnitTest;

[TestFixture]
public sealed class FileTreeNodeTests
{
    [Test]
    public void CloneCreatesIndependentTree()
    {
        FileTreeNode original = FileTreeNode.Folder("src")
            .SetIconCssClass("bi-folder")
            .AddChild(FileTreeNode.File("app.cs"));

        FileTreeNode clone = original.Clone();
        original.AddChild(FileTreeNode.File("new.cs"));

        Assert.Multiple(() =>
        {
            Assert.That(clone, Is.Not.SameAs(original));
            Assert.That(clone.Name, Is.EqualTo("src"));
            Assert.That(clone.IconCssClass, Is.EqualTo("bi-folder"));
            Assert.That(clone.Children, Has.Count.EqualTo(1));
            Assert.That(original.Children, Has.Count.EqualTo(2));
        });
    }

    [Test]
    public void FileCannotContainChildren()
    {
        FileTreeNode file = FileTreeNode.File("README.md");

        Assert.That(() => file.AddChild(FileTreeNode.File("child.txt")), Throws.TypeOf<InvalidOperationException>());
    }

    [Test]
    public void FolderCreatesDirectoryAndCanAddChildren()
    {
        FileTreeNode folder = FileTreeNode.Folder(".config")
            .AddChild(FileTreeNode.File("settings.json").SetDescription("Configuration file"));

        Assert.Multiple(() =>
        {
            Assert.That(folder.Name, Is.EqualTo(".config"));
            Assert.That(folder.IsDirectory, Is.True);
            Assert.That(folder.IsSpecialDirectory, Is.True);
            Assert.That(folder.Children, Has.Count.EqualTo(1));
            Assert.That(folder.Children[0].Name, Is.EqualTo("settings.json"));
            Assert.That(folder.Children[0].Description, Is.EqualTo("Configuration file"));
        });
    }
}