namespace HelpScout.Customers.Websites
{
    public class WebsiteCreateRequest
    {
        public string Value { get; set; }
    }

    public class WebsiteDetail
    {
        public long Id { get; set; }
        public string Value { get; set; }
    }

    public class WebsiteListItem
    {
        public long Id { get; set; }
        public string Value { get; set; }
    }
}