using System.Threading.Tasks;
using HelpScout.Conversations.Models.Detail;

namespace HelpScout.MailBoxes
{
    public sealed class MailboxEndpoint : EndpointBase
    {
        public MailboxEndpoint(IHelpScoutApiClient client) : base(client)
        {
        }

        public async Task<ConversationDetail> Get(long id)
        {
            var resource = await GetResource<ConversationDetail>(id.ToString());
            return resource.WithValidation();
        }

        public async Task<PagedResult<MailboxFieldListitem>> GetFields(long mailboxId)
        {
            var response =
                await GetCollectionInternal<MailboxFieldListitem, object>($"mailboxes/{mailboxId}/fields", null);
            return response.WithValidation();
        }

        public async Task<PagedResult<MailboxFolderListitem>> GetFolders(long mailboxId)
        {
            var response =
                await GetCollectionInternal<MailboxFolderListitem, object>($"mailboxes/{mailboxId}/folders", null);
            return response.WithValidation();
        }


        public async Task<PagedResult<MailboxDetail>> List()
        {
            var response = await GetCollection<MailboxDetail, object>(null);
            return response.WithValidation();
        }


        protected override string GetEndPointName()
        {
            return "mailboxes";
        }
    }
}