using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using HelpScout.Conversations;
using HelpScout.Conversations.Threads.Models.Create;
using Xunit;
using Xunit.Abstractions;

namespace HelpScout.Tests.Threads
{
    [Collection("HelpScout collection")]
    public class ThreadEndpointTests : EndpointTestBase
    {
        public ThreadEndpointTests(HelpScoutClientFixture setup, ITestOutputHelper helper) : base(setup, helper)
        {
            conversationEndpoint = Client.Conversations;
        }

        private readonly ConversationEndpoint conversationEndpoint;

        private async Task<Attachment> GetAttachment()
        {
            var picsumUrl = Faker.Image.PicsumUrl();
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(picsumUrl);

            var ms = new MemoryStream();
            await response.Content.CopyToAsync(ms);
            return new Attachment
            {
                FileName = Faker.System.FileName(),
                MimeType = response.Content.Headers.GetValues("Content-Type").FirstOrDefault(),
                Data = Convert.ToBase64String(ms.ToArray())
            };
        }

        [Fact]
        public async Task Should_be_able_to_create_various_threads_and_verify()
        {
            var req = Setup.ConversationCreateRequest();
            var conversationId = await conversationEndpoint.Create(req);

            var threadEndpoint = conversationEndpoint.Endpoints.Threads(conversationId);

            var customer = Setup.CreateCustomerRequest();
            var customerId = await Client.Customers.Create(customer);
            var thread = new CreateThreadRequest
            {
                Text = Faker.Rant.Review("iphone")
            };
            await threadEndpoint.CreateCustomerThread(thread, customerId);

            thread.Text = Faker.Rant.Review();
            await threadEndpoint.CreateChatThread(thread, customerId);

            thread.Text = Faker.Rant.Review();
            await threadEndpoint.CreateNoteThread(thread);

            thread.Text = Faker.Rant.Review();
            await threadEndpoint.CreatePhoneThread(thread, customerId);

            thread.Text = "Look I have attachment!";
            thread.Attachments = new List<Attachment>
            {
                await GetAttachment()
            };
            await threadEndpoint.CreateReplyThread(thread, customerId);


            //verify
            var threadList = await threadEndpoint.List();
            threadList.Items.Should().HaveCountGreaterOrEqualTo(2);
        }
    }
}