#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

using System;

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Defines web comic social platform options used by web comic component rendering.
    /// </summary>
    [Flags]
    public enum WebComicSocialPlatform
    {
        /// <summary>
        ///     Represents the none option for web comic rendering.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Represents the x option for web comic rendering.
        /// </summary>
        X = 1,

        /// <summary>
        ///     Represents the facebook option for web comic rendering.
        /// </summary>
        Facebook = 2,

        /// <summary>
        ///     Represents the instagram option for web comic rendering.
        /// </summary>
        Instagram = 4,

        /// <summary>
        ///     Represents the linked in option for web comic rendering.
        /// </summary>
        LinkedIn = 8,

        /// <summary>
        ///     Represents the stories option for web comic rendering.
        /// </summary>
        Stories = 16,

        /// <summary>
        ///     Represents the all option for web comic rendering.
        /// </summary>
        All = X | Facebook | Instagram | LinkedIn | Stories
    }
}