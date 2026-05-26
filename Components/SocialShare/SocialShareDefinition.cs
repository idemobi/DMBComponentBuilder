namespace DMBComponentBuilder
{
    /// <summary>
    /// Describes the content and targets rendered by <see cref="SocialShareBuilder"/>.
    /// </summary>
    public class SocialShareDefinition
    {
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public SocialShareKind Kind { get; set; } = SocialShareKind.Html;
        public SocialShareStyle Style { get; set; } = SocialShareStyle.Toolbar;
        public string CssClass { get; set; } = string.Empty;
        public List<SocialSharePlatform> Platforms { get; } = new();
        public Dictionary<SocialSharePlatform, string> Labels { get; } = new();

        public SocialShareDefinition()
        {
            UseDefaultPlatforms();
        }

        public SocialShareDefinition(
            string title,
            string url,
            SocialShareKind kind = SocialShareKind.Html,
            SocialShareStyle style = SocialShareStyle.Toolbar)
        {
            Title = title;
            Url = url;
            Kind = kind;
            Style = style;
            UseDefaultPlatforms();
        }

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

        public SocialShareDefinition UseAllPlatforms()
        {
            Platforms.Clear();
            Platforms.AddRange(Enum.GetValues<SocialSharePlatform>());
            return this;
        }

        public SocialShareDefinition SetPlatforms(params SocialSharePlatform[] platforms)
        {
            Platforms.Clear();
            Platforms.AddRange(platforms.Distinct());
            return this;
        }

        public SocialShareDefinition AddPlatform(SocialSharePlatform platform)
        {
            if (!Platforms.Contains(platform))
            {
                Platforms.Add(platform);
            }

            return this;
        }

        public SocialShareDefinition RemovePlatform(SocialSharePlatform platform)
        {
            Platforms.Remove(platform);
            return this;
        }

        public SocialShareDefinition SetLabel(SocialSharePlatform platform, string label)
        {
            Labels[platform] = label;
            return this;
        }
    }
}
