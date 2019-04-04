using System.Linq;
using System.Threading.Tasks;
using HelpScout.Customers;
using HelpScout.Customers.Phones;
using Xunit;
using Xunit.Abstractions;

namespace HelpScout.Tests.Customers
{
    [Collection("HelpScout collection")]
    public class PhoneEndpointTests : EndpointTestBase
    {
        public PhoneEndpointTests(HelpScoutClientFixture setup, ITestOutputHelper helper) : base(setup, helper)
        {
            endpoint = setup.ApiClient.Customers;
        }

        private readonly CustomerEndpoint endpoint;

        [Fact]
        public async Task Should_create_phone_update_and_then_delete()
        {
            var req = Setup.CreateCustomerRequest();
            var customerId = await endpoint.Create(req);

            //Create Phone

            var phoneApi = endpoint.Endpoints.Phones(customerId);
            var areq = new PhoneCreateRequest
            {
                Type = PhoneType.Home,
                Value = Faker.Phone.PhoneNumber()
            };
            await phoneApi.Create(areq);


            //List Phones

            var savedPhones = await phoneApi.List();
            Assert.Contains(savedPhones.Items, a => a.Type == areq.Type.ToString().ToLowerInvariant()
                                                    && a.Value == areq.Value);


            //Update 
            var createdPhone = savedPhones.Items.First(x => x.Value == areq.Value);
            areq.Type = PhoneType.Work;
            await phoneApi.Update(createdPhone.Id, areq);

            //verify
            savedPhones = await phoneApi.List();
            Assert.Contains(savedPhones.Items, a => a.Type == areq.Type.ToString().ToLowerInvariant()
                                                    && a.Value == areq.Value);


            //delete phone
            await phoneApi.Delete(createdPhone.Id);

            //verify
            savedPhones = await phoneApi.List();
            Assert.Empty(savedPhones.Items);
        }
    }
}