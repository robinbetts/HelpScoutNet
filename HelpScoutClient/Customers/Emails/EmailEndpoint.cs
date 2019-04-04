namespace HelpScout.Customers.Emails
{
    public sealed class EmailEndpoint : CustomerChildEndpointBase<EmailCreateRequest, EmailListItem>
    {
        public EmailEndpoint(long customerId, IHelpScoutApiClient client) :
            base(customerId, "emails", client)
        {
        }
    }
}