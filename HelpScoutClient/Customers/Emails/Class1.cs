namespace HelpScout.Customers.Emails
{
    public class EmailCreateRequest
    {
        public string Value { get; set; }
        public EmailType Type { get; set; } = EmailType.Home;
    }

    public class EmailDetail
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }

    public enum EmailType
    {
        Home = 1,
        Work,
        Other
    }

    public class EmailListItem
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }
}