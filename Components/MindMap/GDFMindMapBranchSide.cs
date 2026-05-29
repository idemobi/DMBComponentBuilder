#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Defines the visual side used to place a <see cref="GDFMindMapNode" /> branch.
    /// </summary>
    public enum GDFMindMapBranchSide
    {
        /// <summary>
        ///     Lets <see cref="GDFMindMapBuilder" /> balance branches automatically.
        /// </summary>
        Auto,

        /// <summary>
        ///     Places the branch on the left side of the central topic.
        /// </summary>
        Left,

        /// <summary>
        ///     Places the branch on the right side of the central topic.
        /// </summary>
        Right
    }
}