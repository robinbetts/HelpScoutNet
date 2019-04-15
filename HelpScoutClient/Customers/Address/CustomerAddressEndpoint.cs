using System.Threading.Tasks;

namespace HelpScout.Customers.Address
{
    public sealed class CustomerAddressEndpoint : EndpointBase
    {
        private readonly long customerId;

        public CustomerAddressEndpoint(long customerId, IHelpScoutApiClient client) : base(client)
        {
            this.customerId = customerId;
        }

        public async Task Create(AddressCreateRequest req)
        {
            var resource = await CreateResource(req).ConfigureAwait(false);
            resource.WithValidation();
        }

        public async Task Delete()
        {
            var resource = await DeleteResource(null).ConfigureAwait(false);
            resource.WithValidation();
        }

        public async Task<AddressDetail> Get()
        {
            var resource = await GetResource<AddressDetail>(null).ConfigureAwait(false);
            return resource.WithValidation();
        }


        public async Task Update(AddressCreateRequest req)
        {
            var resource = await UpdateResource(null, req).ConfigureAwait(false);
            resource.WithValidation();
        }

        protected override string GetEndPointName()
        {
            return $"customers/{customerId}/address";
        }

        //Search is not supported by this api
    }
}