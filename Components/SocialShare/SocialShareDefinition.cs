#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Describes the content and targets rendered by <see cref="SocialShareBuilder" />.
    /// </summary>
    public class SocialShareDefinition
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets css class used by social share component rendering.
        /// </summary>
        public string CssClass { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets kind used by social share component rendering.
        /// </summary>
        public SocialShareKind Kind { get; set; } = SocialShareKind.Html;

        /// <summary>
        ///     Gets the custom labels keyed by social platform.
        /// </summary>
        public Dictionary<SocialSharePlatform, string> Labels { get; } = new();

        /// <summary>
        ///     Gets the social platforms rendered by the share component.
        /// </summary>
        public List<SocialSharePlatform> Platforms { get; } = new();

        /// <summary>
        ///     Gets or sets style used by social share component rendering.
        /// </summary>
        public SocialShareStyle Style { get; set; } = SocialShareStyle.Toolbar;

        /// <summary>
        ///     Gets or sets title used by social share component rendering.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets url used by social share component rendering.
        /// </summary>
        public string Url { get; set; } = string.Empty;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SocialShareDefinition" /> class.
        /// </summary>
        public SocialShareDefinition()
        {
            UseDefaultPlatforms();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SocialShareDefinition" /> class.
        /// </summary>
        /// <param name="title">The title value.</param>
        /// <param name="url">The url value.</param>
        /// <param name="kind">The kind value.</param>
        /// <param name="style">The style value.</param>
        public SocialShareDefinition(
            string title,
            string url,
            SocialShareKind kind = SocialShareKind.Html,
            SocialShareStyle style = SocialShareStyle.Toolbar
        )
        {
            Title = title;
            Url = url;
            Kind = kind;
            Style = style;
            UseDefaultPlatforms();
        }

        #endregion

        #region Instance methods

        /// <summary>
        ///     Adds platform to the social share component.
        /// </summary>
        /// <param name="platform">The platform value.</param>
        /// <returns>The generated social share value.</returns>
        public SocialShareDefinition AddPlatform(SocialSharePlatform platform)
        {
            if (!Platforms.Contains(platform))
            {
                Platforms.Add(platform);
            }

            return this;
        }

        /// <summary>
        ///     Removes platform from the social share component.
        /// </summary>
        /// <param name="platform">The platform value.</param>
        /// <returns>The generated social share value.</returns>
        public SocialShareDefinition RemovePlatform(SocialSharePlatform platform)
        {
            Platforms.Remove(platform);
            return this;
        }

        /// <summary>
        ///     Configures the label for the social share component.
        /// </summary>
        /// <param name="platform">The platform value.</param>
        /// <param name="label">The label value.</param>
        /// <returns>The generated social share value.</returns>
        public SocialShareDefinition SetLabel(SocialSharePlatform platform, string label)
        {
            Labels[platform] = label;
            return this;
        }

        /// <summary>
        ///     Configures the platforms for the social share component.
        /// </summary>
        /// <param name="platforms">The social platforms included in generated share previews.</param>
        /// <returns>The generated social share value.</returns>
        public SocialShareDefinition SetPlatforms(params SocialSharePlatform[] platforms)
        {
            Platforms.Clear();
            Platforms.AddRange(platforms.Distinct());
            return this;
        }

        /// <summary>
        ///     Configures whether the all platforms option is used by the social share component.
        /// </summary>
        /// <returns>The generated social share value.</returns>
        public SocialShareDefinition UseAllPlatforms()
        {
            Platforms.Clear();
            Platforms.AddRange(Enum.GetValues<SocialSharePlatform>());
            return this;
        }

        /// <summary>
        ///     Configures whether the default platforms option is used by the social share component.
        /// </summary>
        /// <returns>The generated social share value.</returns>
        public SocialShareDefinition UseDefaultPlatforms()
        {
            Platforms.Clear();
            Platforms.AddRange(new[]
            {
                SocialSharePlatform.Email,
                SocialSharePlatform.Facebook,
                SocialSharePlatform.WhatsApp,
                SocialSharePlatform.Reddit,
                SocialSharePlatform.Twitter,
                SocialSharePlatform.LinkedIn,
                SocialSharePlatform.Discord
            });

            return this;
        }

        #endregion
    }
}