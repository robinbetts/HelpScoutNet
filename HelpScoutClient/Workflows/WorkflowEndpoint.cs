using System.Threading.Tasks;
using HelpScout.Users;

namespace HelpScout.Workflows
{
    public class WorkflowEndpoint : EndpointBase
    {
        public WorkflowEndpoint(IHelpScoutApiClient client) : base(client)
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

        public async Task UpdateStatus(long workflowId, WorkflowStatus status)
        {
            var data = new
            {
                value = status.ToString().ToLowerInvariant(),
                op = "replace",
                path = "/status"
            };
            var response = await Patch<object>($"/workflows/{workflowId}", data).ConfigureAwait(false);
            response.WithValidation();
        }

        protected override string GetEndPointName()
        {
            return "workflows";
        }
    }
}