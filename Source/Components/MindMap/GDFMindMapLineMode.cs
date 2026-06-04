#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Defines how connector lines are rendered between <see cref="GDFMindMapNode" /> topics.
    /// </summary>
    public enum GDFMindMapLineMode
    {
        /// <summary>
        ///     Draws a direct point-to-point line between connected topics.
        /// </summary>
        Straight,

        /// <summary>
        ///     Draws straight horizontal, vertical, and horizontal segments.
        /// </summary>
        Orthogonal,

        /// <summary>
        ///     Draws horizontal, rounded corner, vertical, rounded corner, and horizontal segments.
        /// </summary>
        RoundedOrthogonal,

        /// <summary>
        ///     Draws a Bezier curve with horizontal control vectors using twenty percent of the available margin.
        /// </summary>
        WeightedBezier
    }
}