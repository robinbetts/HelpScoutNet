using System.Threading.Tasks;
using HelpScout.MailBoxes;
using Xunit;
using Xunit.Abstractions;

namespace HelpScout.Tests.Mailboxes
{
    [Collection("HelpScout collection")]
    public class MailboxEndpointTests : EndpointTestBase
    {
        public MailboxEndpointTests(HelpScoutClientFixture setup, ITestOutputHelper helper) : base(setup, helper)
        {
            endpoint = Client.Mailboxes;
        }

        private readonly MailboxEndpoint endpoint;


        [Fact]
        public async Task Should_list_all_the_mailboxes()
        {
            var what = await endpoint.List();
            Assert.NotEmpty(what.Items);
            Assert.Contains(what.Items, a => a.Id == Constants.MailBoxId);
            Assert.True(what != null);
        }


        [Fact]
        public async Task Should_list_specified_mailbox_custom_fields()
        {
            var what = await endpoint.GetFields(Constants.MailBoxId);
            Assert.NotEmpty(what.Items);
            Assert.Contains(what.Items, a => a.Name.Equals("Selz_field"));
        }

        [Fact]
        public async Task Should_list_specified_mailbox_folders()
        {
            var what = await endpoint.GetFolders(Constants.MailBoxId);
            Assert.NotEmpty(what.Items);
        }
    }
}