namespace HelpScout.Customers.Websites
{
    public sealed class WebsiteEndpoint : CustomerChildEndpointBase<WebsiteCreateRequest, WebsiteListItem>
    {
        public WebsiteEndpoint(long customerId, IHelpScoutApiClient client) :
            base(customerId, "websites", client)
        {
        }
    }
}