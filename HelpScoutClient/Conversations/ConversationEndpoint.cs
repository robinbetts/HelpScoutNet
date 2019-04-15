using System.Collections.Generic;
using System.Threading.Tasks;
using HelpScout.Conversations.Models.Create;
using HelpScout.Conversations.Models.Detail;
using HelpScout.Conversations.Models.List;
using HelpScout.Conversations.Threads;

namespace HelpScout.Conversations
{
    public sealed class ConversationEndpoint : EndpointBase
    {
        public ConversationEndpoint(IHelpScoutApiClient client) : base(client)
        {
        }

        public ChildEndpoints Endpoints => new ChildEndpoints(Client);

        public async Task<ConversationDetail> Get(long id)
        {
            var resource = await GetResource<ConversationDetail>(id.ToString()).ConfigureAwait(false);
            return resource.WithValidation();
        }

        public async Task<long> Create(ConversationCreateRequest req)
        {
            var resource = await CreateResource(req).ConfigureAwait(false);
            resource.WithValidation();
            return long.Parse(resource.GetHeaderValueSingle("Resource-Id"));
        }

        public async Task Delete(long id)
        {
            var resource = await DeleteResource(id.ToString()).ConfigureAwait(false);
            resource.WithValidation();
        }

        public async Task UpdateTags(long conversationid, IList<string> tags)
        {
            tags = tags ?? new List<string>();
            var resource = await RequestSingle<object>($"conversations/{conversationid}/tags", tags).ConfigureAwait(false);
            resource.WithValidation();
        }

        public Task RemoveTags(long conversationid)
        {
            return UpdateTags(conversationid, null);
        }

        public async Task<PagedResult<ConversationListItem>> List(ConversationSearchQuery query)
        {
            var response = await GetCollection<ConversationListItem, ConversationSearchQuery>(query).ConfigureAwait(false);
            return response.WithValidation();
        }

        public async Task ChangeSubject(long conversationId, string subject)
        {
            var payload = new
            {
                op = "replace",
                path = "/subject",
                value = subject
            };
            await PerformPatch(conversationId, payload).ConfigureAwait(false);
        }

        public async Task ChangeCustomer(long conversationId, long customerId)
        {
            var payload = new
            {
                op = "replace",
                path = "/primaryCustomer.id",
                value = customerId
            };
            await PerformPatch(conversationId, payload).ConfigureAwait(false);
        }

        public async Task PublishDraft(long conversationId)
        {
            var payload = new
            {
                op = "replace",
                path = "/draft",
                value = true
            };
            await PerformPatch(conversationId, payload).ConfigureAwait(false);
        }

        public async Task MoveToMailbox(long conversationId, long mailboxId)
        {
            var payload = new
            {
                op = "move",
                path = "/mailboxId",
                value = mailboxId
            };
            await PerformPatch(conversationId, payload).ConfigureAwait(false);
        }

        public async Task ChangeStatus(long conversationId, ConversationStatus status)
        {
            var payload = new
            {
                op = "replace",
                path = "/status",
                value = status.ToString().ToLowerInvariant()
            };
            await PerformPatch(conversationId, payload).ConfigureAwait(false);
        }

        public async Task ChangeOwner(long conversationId, long ownerId)
        {
            var payload = new
            {
                op = "replace",
                path = "/assignTo",
                value = ownerId
            };
            await PerformPatch(conversationId, payload).ConfigureAwait(false);
        }

        public async Task RemoveOwner(long conversationId, long ownerId)
        {
            var payload = new
            {
                op = "remove",
                path = "/assignTo",
                value = ownerId
            };
            await PerformPatch(conversationId, payload).ConfigureAwait(false);
        }


        private async Task PerformPatch(long conversationId, object payload)
        {
            var response = await Patch<object>($"/conversations/{conversationId}", payload).ConfigureAwait(false);
            response.WithValidation();
        }


        protected override string GetEndPointName()
        {
            return "conversations";
        }

        public class ChildEndpoints
        {
            private readonly IHelpScoutApiClient client;


            public ChildEndpoints(IHelpScoutApiClient client)
            {
                this.client = client;
            }

            public ThreadEndpoint Threads(long conversationId)
            {
                return new ThreadEndpoint(conversationId, client);
            }
        }
    }
}