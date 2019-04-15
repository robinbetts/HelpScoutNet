using System.Threading.Tasks;

namespace HelpScout.Tags
{
    public class TagsEndpoint : EndpointBase
    {
        public TagsEndpoint(IHelpScoutApiClient client) : base(client)
        {
        }

        public async Task<PagedResult<TagDetail>> List()
        {
            var response = await GetCollection<TagDetail, object>(null).ConfigureAwait(false);
            return response.WithValidation();
        }

        protected override string GetEndPointName()
        {
            return "tags";
        }
    }
}