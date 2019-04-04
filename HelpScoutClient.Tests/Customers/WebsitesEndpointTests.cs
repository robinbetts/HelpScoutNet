using System.Linq;
using System.Threading.Tasks;
using HelpScout.Customers;
using HelpScout.Customers.Websites;
using Xunit;
using Xunit.Abstractions;

namespace HelpScout.Tests.Customers
{
    [Collection("HelpScout collection")]
    public class WebsitesEndpointTests : EndpointTestBase
    {
        public WebsitesEndpointTests(HelpScoutClientFixture setup, ITestOutputHelper helper) : base(setup, helper)
        {
            endpoint = setup.ApiClient.Customers;
        }

        private readonly CustomerEndpoint endpoint;

        [Fact]
        public async Task Should_create_website_update_and_then_delete()
        {
            var req = Setup.CreateCustomerRequest();
            var customerId = await endpoint.Create(req);

            //Create

            var websiteApi = endpoint.Endpoints.Websites(customerId);
            var areq = new WebsiteCreateRequest
            {
                Value = Faker.Internet.Avatar()
            };
            await websiteApi.Create(areq);


            //List

            var savedWebsite = await websiteApi.List();
            Assert.Contains(savedWebsite.Items, a => a.Value == areq.Value);


            //Update 
            var createdWebsite = savedWebsite.Items.First(x => x.Value == areq.Value);
            areq.Value = Faker.Internet.Avatar();
            await websiteApi.Update(createdWebsite.Id, areq);

            //verify
            savedWebsite = await websiteApi.List();
            Assert.Contains(savedWebsite.Items, a => a.Value == areq.Value);


            //delete
            await websiteApi.Delete(createdWebsite.Id);

            //verify
            savedWebsite = await websiteApi.List();
            Assert.Empty(savedWebsite.Items);
        }
    }
}