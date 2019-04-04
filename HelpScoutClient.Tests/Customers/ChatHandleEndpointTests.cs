using System.Linq;
using System.Threading.Tasks;
using HelpScout.Customers;
using HelpScout.Customers.ChatHandles;
using Xunit;
using Xunit.Abstractions;

namespace HelpScout.Tests.Customers
{
    [Collection("HelpScout collection")]
    public class ChatHandleEndpointTests : EndpointTestBase
    {
        public ChatHandleEndpointTests(HelpScoutClientFixture setup, ITestOutputHelper helper) : base(setup, helper)
        {
            endpoint = setup.ApiClient.Customers;
        }

        private readonly CustomerEndpoint endpoint;

        [Fact]
        public async Task Should_create_chat_handle_update_and_then_delete()
        {
            var req = Setup.CreateCustomerRequest();
            var customerId = await endpoint.Create(req);

            //Create 

            var chatHandleApi = endpoint.Endpoints.ChatHandles(customerId);
            var areq = new ChatHandleCreateRequest
            {
                Type = ChatHandleType.Gtalk,
                Value = Faker.Internet.Email()
            };
            await chatHandleApi.Create(areq);


            //List 

            var savedChatHandles = await chatHandleApi.List();
            Assert.Contains(savedChatHandles.Items, a => a.Type == areq.Type.ToString().ToLowerInvariant()
                                                         && a.Value == areq.Value);


            //Update 
            var createdChatHandle = savedChatHandles.Items.First(x => x.Value == areq.Value);
            areq.Type = ChatHandleType.Skype;
            await chatHandleApi.Update(createdChatHandle.Id, areq);

            //verify
            savedChatHandles = await chatHandleApi.List();
            Assert.Contains(savedChatHandles.Items, a => a.Type == areq.Type.ToString().ToLowerInvariant()
                                                         && a.Value == areq.Value);


            //delete
            await chatHandleApi.Delete(createdChatHandle.Id);

            //verify
            savedChatHandles = await chatHandleApi.List();
            Assert.Empty(savedChatHandles.Items);
        }
    }
}