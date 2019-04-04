using System.Linq;
using System.Threading.Tasks;
using HelpScout.Customers;
using Xunit;
using Xunit.Abstractions;

namespace HelpScout.Tests.Customers
{
    [Collection("HelpScout collection")]
    public class CustomerEndpointTests : EndpointTestBase
    {
        public CustomerEndpointTests(HelpScoutClientFixture setup, ITestOutputHelper helper) : base(setup, helper)
        {
            endpoint = Client.Customers;
        }

        private readonly CustomerEndpoint endpoint;


        [Fact]
        public async Task Customer_list_should_not_be_null()
        {
            var what = await endpoint.List(new CustomerSearchQuery());
            Assert.True(what != null);
        }

        [Fact]
        public async Task Should_create_one_customer_and_update_immediately()
        {
            var req = Setup.CreateCustomerRequest();
            var id = await endpoint.Create(req);
            Assert.True(id > 0);
            var customer = await endpoint.Get(id);

            Assert.Equal(req.FirstName, customer.FirstName);
            Assert.Equal(req.LastName, customer.LastName);
            Assert.Equal(req.Age, customer.Age);

            var firstEmail = req.Emails.First();

            Assert.Contains(customer.Embedded.Emails,
                a => a.Type == firstEmail.Type && a.Value == firstEmail.Value);

            req.FirstName = req.FirstName + " Changed";
            await endpoint.Update(id, req);
            customer = await endpoint.Get(id);
            Assert.Equal(req.FirstName, customer.FirstName);
        }
    }
}