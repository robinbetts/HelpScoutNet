using System.Linq;
using System.Threading.Tasks;
using HelpScout.Customers;
using HelpScout.Customers.SocialProfiles;
using Xunit;
using Xunit.Abstractions;

namespace HelpScout.Tests.Customers
{
    [Collection("HelpScout collection")]
    public class SocialProfileEndpointTests : EndpointTestBase
    {
        public SocialProfileEndpointTests(HelpScoutClientFixture setup, ITestOutputHelper helper) : base(setup, helper)
        {
            endpoint = setup.ApiClient.Customers;
        }

        private readonly CustomerEndpoint endpoint;

        [Fact]
        public async Task Should_create_social_profile_update_and_then_delete()
        {
            var req = Setup.CreateCustomerRequest();
            var customerId = await endpoint.Create(req);

            //Create

            var profileApi = endpoint.Endpoints.SocialProfiles(customerId);
            var areq = new SocialProfileCreateRequest
            {
                Type = SocialProfileType.Facebook,
                Value = Faker.Internet.Avatar()
            };
            await profileApi.Create(areq);


            //List , Verify

            var savedProfiles = await profileApi.List();
            Assert.Contains(savedProfiles.Items, a => a.Type == areq.Type.ToString().ToLowerInvariant()
                                                      && a.Value == areq.Value);


            //Update 
            var createdPhone = savedProfiles.Items.First(x => x.Value == areq.Value);
            areq.Type = SocialProfileType.Google;
            areq.Value = Faker.Internet.Avatar();
            await profileApi.Update(createdPhone.Id, areq);

            //verify
            savedProfiles = await profileApi.List();
            Assert.Contains(savedProfiles.Items, a => a.Type == areq.Type.ToString().ToLowerInvariant()
                                                      && a.Value == areq.Value);


            //delete
            await profileApi.Delete(createdPhone.Id);

            //verify
            savedProfiles = await profileApi.List();
            Assert.Empty(savedProfiles.Items);
        }
    }
}