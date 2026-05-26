namespace DMBComponentBuilder
{
    [Flags]
    public enum WebComicSocialPlatform
    {
        None = 0,
        X = 1,
        Facebook = 2,
        Instagram = 4,
        LinkedIn = 8,
        Stories = 16,
        All = X | Facebook | Instagram | LinkedIn | Stories
    }
}
