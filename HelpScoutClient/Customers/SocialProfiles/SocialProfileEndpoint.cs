namespace HelpScout.Customers.SocialProfiles
{
    public sealed class
        SocialProfileEndpoint : CustomerChildEndpointBase<SocialProfileCreateRequest, SocialProfileListItem>
    {
        public SocialProfileEndpoint(long customerId, IHelpScoutApiClient client) :
            base(customerId, "social-profiles", client)
        {
        }
    }
}