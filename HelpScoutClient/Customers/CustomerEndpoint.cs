using System.Threading.Tasks;
using HelpScout.Customers.Address;
using HelpScout.Customers.ChatHandles;
using HelpScout.Customers.Emails;
using HelpScout.Customers.Phones;
using HelpScout.Customers.SocialProfiles;
using HelpScout.Customers.Websites;

namespace HelpScout.Customers
{
    public sealed class CustomerEndpoint : EndpointBase
    {
        public CustomerEndpoint(IHelpScoutApiClient client) : base(client)
        {
        }

        public ChildEndpoints Endpoints => new ChildEndpoints(Client);


        public async Task<CustomerDetail> Get(long id)
        {
            var resource = await GetResource<CustomerDetail>(id.ToString()).ConfigureAwait(false);
            return resource.WithValidation();
        }

        public async Task<PagedResult<CustomerListItem>> List(CustomerSearchQuery query)
        {
            var response = await GetCollection<CustomerListItem, CustomerSearchQuery>(query).ConfigureAwait(false);
            return response.WithValidation();
        }


        public async Task<long> Create(CustomerCreateRequest req)
        {
            var resource = await CreateResource(req).ConfigureAwait(false);
            resource.WithValidation();
            return long.Parse(resource.GetHeaderValueSingle("Resource-Id"));
        }

        public async Task Update(long id, CustomerCreateRequest req)
        {
            var resource = await UpdateResource(id.ToString(), req).ConfigureAwait(false);
            resource.WithValidation();
        }

        protected override string GetEndPointName()
        {
            return "customers";
        }

        public class ChildEndpoints
        {
            private readonly IHelpScoutApiClient client;


            public ChildEndpoints(IHelpScoutApiClient client)
            {
                this.client = client;
            }

            public CustomerAddressEndpoint Address(long customerId)
            {
                return new CustomerAddressEndpoint(customerId, client);
            }

            public PhoneEndpoint Phones(long customerId)
            {
                return new PhoneEndpoint(customerId, client);
            }

            public EmailEndpoint Emails(long customerId)
            {
                return new EmailEndpoint(customerId, client);
            }

            public ChatHandleEndpoint ChatHandles(long customerId)
            {
                return new ChatHandleEndpoint(customerId, client);
            }

            public SocialProfileEndpoint SocialProfiles(long customerId)
            {
                return new SocialProfileEndpoint(customerId, client);
            }

            public WebsiteEndpoint Websites(long customerId)
            {
                return new WebsiteEndpoint(customerId, client);
            }
        }
    }
}