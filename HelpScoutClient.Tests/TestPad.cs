using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace HelpScout.Tests
{
    [Collection("HelpScout collection")]
    public class TestPad : EndpointTestBase
    {
        public TestPad(HelpScoutClientFixture setup, ITestOutputHelper helper) : base(setup, helper)
        {
        }

        [Fact]
        public void Credentials_should_not_be_empty()
        {
            Setup.Credentials.Should().NotBeNull();

            Setup.Credentials.ClientId.Should().NotBeEmpty();
            Setup.Credentials.ClientSecret.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Token_should_not_be_null()
        {
            var a = await Setup.ApiClient.GetToken();
            a.Should().NotBeNull("This should have been already created.");
        }
    }
}