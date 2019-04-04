namespace HelpScout.Customers.Phones
{
    public sealed class PhoneEndpoint : CustomerChildEndpointBase<PhoneCreateRequest, PhoneListItem>
    {
        public PhoneEndpoint(long customerId, IHelpScoutApiClient client) :
            base(customerId, "phones", client)
        {
        }
    }
}