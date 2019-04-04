using System.Linq;
using System.Threading.Tasks;
using HelpScout.Customers;
using HelpScout.Customers.Emails;
using Xunit;
using Xunit.Abstractions;

namespace HelpScout.Tests.Customers
{
    [Collection("HelpScout collection")]
    public class EmailEndpointTests : EndpointTestBase
    {
        public EmailEndpointTests(HelpScoutClientFixture setup, ITestOutputHelper helper) : base(setup, helper)
        {
            endpoint = setup.ApiClient.Customers;
        }

        private readonly CustomerEndpoint endpoint;

        [Fact]
        public async Task Should_create_email_update_and_then_delete()
        {
            var req = Setup.CreateCustomerRequest();
            req.Emails = null;

            var customerId = await endpoint.Create(req);

            //Create 

            var emailApi = endpoint.Endpoints.Emails(customerId);
            var areq = new EmailCreateRequest
            {
                Type = EmailType.Home,
                Value = Faker.Internet.Email()
            };
            await emailApi.Create(areq);


            //List 

            var savedEmails = await emailApi.List();
            Assert.Contains(savedEmails.Items, a => a.Type == areq.Type.ToString().ToLowerInvariant()
                                                    && a.Value == areq.Value);


            //Update 
            var createdEmail = savedEmails.Items.First(x => x.Value == areq.Value);
            areq.Type = EmailType.Work;
            areq.Value = Faker.Internet.Email(Faker.Person.FirstName, Faker.Person.LastName);

            await emailApi.Update(createdEmail.Id, areq);

            //verify
            savedEmails = await emailApi.List();
            Assert.Contains(savedEmails.Items, a => a.Type == areq.Type.ToString().ToLowerInvariant()
                                                    && a.Value == areq.Value);


            //delete
            await emailApi.Delete(createdEmail.Id);

            //verify
            savedEmails = await emailApi.List();
            Assert.Empty(savedEmails.Items);
        }
    }
}