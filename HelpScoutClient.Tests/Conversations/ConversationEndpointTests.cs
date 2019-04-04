using System.Collections.Generic;
using System.Threading.Tasks;
using HelpScout.Conversations;
using HelpScout.Conversations.Models.Create;
using Xunit;
using Xunit.Abstractions;
using ConversationStatus = HelpScout.Conversations.ConversationStatus;

namespace HelpScout.Tests.Conversations
{
    [Collection("HelpScout collection")]
    public class ConversationEndpointTests : EndpointTestBase
    {
        public ConversationEndpointTests(HelpScoutClientFixture setup, ITestOutputHelper helper) : base(setup, helper)
        {
            conversationEndpoint = Client.Conversations;
        }

        private readonly ConversationEndpoint conversationEndpoint;

        private static ConversationSearchQuery CreateSearchCriteria()
        {
            var query = new ConversationSearchQuery
            {
                MailBoxs = {Constants.MailBoxId.ToString()},
                Status = ConversationStatus.Pending,
                SortField = ConversationSortField.CreatedAt
            };
            return query;
        }


        [Fact]
        public async Task Getting_conversation_from_invalid_id_should_throw_Exception()
        {
            await Assert.ThrowsAsync<HelpScoutException>(() => conversationEndpoint.Get(1254444455));
        }

        [Fact]
        public async Task Should_be_able_to_create_conversation_and_delete()
        {
            var req = Setup.ConversationCreateRequest();
            req.Customer.Email = Setup.Faker.Internet.Email();
            req.Type = ConversationType.Email;
            req.Tags = new List<string> {"test-1"};
            req.Status = HelpScout.Conversations.Models.Create.ConversationStatus.Pending;

            var id = await conversationEndpoint.Create(req);
            var actual = await conversationEndpoint.Get(id);

            Assert.Contains(actual.Tags, a => a.Tag == "test-1");
            Assert.Equal(req.Subject, actual.Subject);
            Assert.Equal("email", actual.Type);
            Assert.Equal(req.MailboxId, actual.MailboxId);
            Assert.Equal("pending", actual.Status);


            await conversationEndpoint.Delete(id);
            Assert.True(true);
        }

        [Fact]
        public async Task Should_have_some_conversation_or_no_Conversation()
        {
            var result = await conversationEndpoint.List(CreateSearchCriteria());
            Assert.True(result.Size == 50);
        }
    }
}