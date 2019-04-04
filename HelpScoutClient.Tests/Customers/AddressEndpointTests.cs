using System.Collections.Generic;
using System.Threading.Tasks;
using HelpScout.Customers;
using HelpScout.Customers.Address;
using Xunit;
using Xunit.Abstractions;

namespace HelpScout.Tests.Customers
{
    [Collection("HelpScout collection")]
    public class AddressEndpointTests : EndpointTestBase
    {
        public AddressEndpointTests(HelpScoutClientFixture setup,ITestOutputHelper helper) : base(setup,helper)
        {
            endpoint = setup.ApiClient.Customers;
        }

        private readonly CustomerEndpoint endpoint;

        [Fact]
        public async Task Should_create_address_update_and_then_delete()
        {
            var req = Setup.CreateCustomerRequest();
            var customerId = await endpoint.Create(req);

            //Create address

            var addressEndpoint = endpoint.Endpoints.Address(customerId);
            var areq = new AddressCreateRequest
            {
                City = Faker.Address.City(),
                CountryCode = Faker.Address.CountryCode(),
                Lines = new List<string>
                {
                    Faker.Address.SecondaryAddress()
                },
                PostalCode = Faker.Address.ZipCode(),
                State = Faker.Address.State()
            };
            await addressEndpoint.Create(areq);


            //Get Address

            var savedAddress = await addressEndpoint.Get();
            Assert.Equal(areq.CountryCode, savedAddress.Country);
            Assert.Equal(areq.City, savedAddress.City);
            Assert.Equal(areq.Lines, savedAddress.Lines);
            Assert.Equal(areq.PostalCode, savedAddress.PostalCode);
            Assert.Equal(areq.State, savedAddress.State);

            //Update address

            areq.City = "Kathmandu";
            areq.CountryCode = "NP";
            areq.PostalCode = "12345";
            await addressEndpoint.Update(areq);

            savedAddress = await addressEndpoint.Get();
            Assert.Equal(areq.CountryCode, savedAddress.Country);
            Assert.Equal(areq.City, savedAddress.City);
            Assert.Equal(areq.Lines, savedAddress.Lines);
            Assert.Equal(areq.PostalCode, savedAddress.PostalCode);
            Assert.Equal(areq.State, savedAddress.State);


            //delete address

            await addressEndpoint.Delete();
            savedAddress = await addressEndpoint.Get();

            Assert.Null(savedAddress.Country);
            Assert.Null(savedAddress.City);
            Assert.Empty(savedAddress.Lines);
            Assert.Null(savedAddress.PostalCode);
            Assert.Null(savedAddress.State);
        }
    }
}