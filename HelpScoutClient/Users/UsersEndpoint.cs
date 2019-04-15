using System.Threading.Tasks;

namespace HelpScout.Users
{
    public class UsersEndpoint : EndpointBase
    {
        public UsersEndpoint(IHelpScoutApiClient client) : base(client)
        {
        }

        public async Task<UserDetail> GetResourceOwner()
        {
            var response = await RequestSingle<UserDetail>("/users/me", null).ConfigureAwait(false);
            return response.WithValidation();
        }


        public async Task<UserDetail> Get(long userId)
        {
            var response = await GetResource<UserDetail>(userId.ToString()).ConfigureAwait(false);
            return response.WithValidation();
        }

        public async Task<PagedResult<UserDetail>> List()
        {
            var response = await GetCollection<UserDetail, object>(null).ConfigureAwait(false);
            return response.WithValidation();
        }

        protected override string GetEndPointName()
        {
            return "users";
        }
    }
}