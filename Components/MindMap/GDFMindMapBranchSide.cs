#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj GDFMindMapBranchSide.cs create at 2026/05/21
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    /// Defines the visual side used to place a <see cref="GDFMindMapNode"/> branch.
    /// </summary>
    public enum GDFMindMapBranchSide
    {
        /// <summary>
        /// Lets <see cref="GDFMindMapBuilder"/> balance branches automatically.
        /// </summary>
        Auto,

        /// <summary>
        /// Places the branch on the left side of the central topic.
        /// </summary>
        Left,

        /// <summary>
        /// Places the branch on the right side of the central topic.
        /// </summary>
        Right
    }
}
