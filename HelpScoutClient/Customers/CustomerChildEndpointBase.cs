using System.Threading.Tasks;

namespace HelpScout.Customers
{
    public class CustomerChildEndpointBase<TCreate, TListItem> : EndpointBase
    {
        private readonly long customerId;
        private readonly string endpointName;

        public CustomerChildEndpointBase(long customerId, string endpointName, IHelpScoutApiClient client) :
            base(client)
        {
            this.customerId = customerId;
            this.endpointName = endpointName;
        }

        public async Task Create(TCreate req)
        {
            var resource = await CreateResource(req);
            resource.WithValidation();
        }

        public async Task Delete(long id)
        {
            var resource = await DeleteResource(id.ToString());
            resource.WithValidation();
        }

        public async Task<PagedResult<TListItem>> List()
        {
            var response = await GetCollection<TListItem, object>(null);
            return response.WithValidation();
        }

        public async Task Update(long id, TCreate req)
        {
            var resource = await UpdateResource(id.ToString(), req);
            resource.WithValidation();
        }

        protected override string GetEndPointName()
        {
            return $"customers/{customerId}/{endpointName}";
        }
    }
}