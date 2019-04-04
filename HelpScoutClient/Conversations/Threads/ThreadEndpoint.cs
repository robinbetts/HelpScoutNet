using System.Net.Http;
using System.Threading.Tasks;
using HelpScout.Conversations.Threads.Models.Create;
using HelpScout.Conversations.Threads.Models.List;

namespace HelpScout.Conversations.Threads
{
    public class ThreadEndpoint : EndpointBase
    {
        private readonly long conversationId;

        public ThreadEndpoint(long conversationId, IHelpScoutApiClient client) : base(client)
        {
            this.conversationId = conversationId;
        }


        private async Task CreateThread(string fragment, CreateThreadRequest req, long? customerId = null)
        {
            object data = null;
            if (customerId != null)
                data = new
                {
                    req.Text,
                    req.Attachments,
                    Customer = new {Id = customerId}
                };
            else
                data = new
                {
                    req.Text,
                    req.Attachments
                };
            var response =
                await RequestSingle<object>($"conversations/{conversationId}/{fragment}", data, HttpMethod.Post);
            response.WithValidation();
        }

        public async Task CreateChatThread(CreateThreadRequest req, long customerId)
        {
            await CreateThread("chats", req, customerId);
        }

        public async Task CreateCustomerThread(CreateThreadRequest req, long customerId)
        {
            await CreateThread("customer", req, customerId);
        }

        public async Task CreateNoteThread(CreateThreadRequest req)
        {
            await CreateThread("notes", req);
        }

        public async Task CreatePhoneThread(CreateThreadRequest req, long customerId)
        {
            await CreateThread("phones", req, customerId);
        }

        public async Task CreateReplyThread(CreateThreadRequest req, long customerId)
        {
            await CreateThread("reply", req, customerId);
        }


        public async Task UpdateThreadContent(long threadId, string text)
        {
            var payload = new
            {
                op = "replace",
                path = "/text",
                value = text
            };
            var response = await Patch<object>($"conversations/{conversationId}/threads/{threadId}", payload);
            response.WithValidation();
        }

        public async Task<PagedResult<ThreadDetail>> List()
        {
            var fragment = $"conversations/{conversationId}/threads";
            var response = await GetCollectionInternal<ThreadDetail, object>(fragment, null);
            return response.WithValidation();
        }

        protected override string GetEndPointName()
        {
            //we are managing our own urls, this should never be called
            return null;
        }
    }
}