namespace HelpScout.Customers.SocialProfiles
{
    public class SocialProfileCreateRequest
    {
        public string Value { get; set; }
        public SocialProfileType Type { get; set; } = SocialProfileType.Aboutme;
    }

    public class SocialProfileDetail
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }

    public enum SocialProfileType
    {
        Aboutme = 1,
        Facebook,
        Flickr,
        Forsquare,
        Google,
        Googleplus,
        Linkedin,
        Other,
        Quora,
        Tungleme,
        Twitter,
        Youtube
    }

    public class SocialProfileListItem
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }
}