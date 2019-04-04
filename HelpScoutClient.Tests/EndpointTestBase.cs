using Bogus;
using Xunit;
using Xunit.Abstractions;

namespace HelpScout.Tests
{
    public abstract class EndpointTestBase : IClassFixture<HelpScoutClientFixture>
    {
        private readonly ITestOutputHelper helper;

        protected EndpointTestBase(HelpScoutClientFixture setup,ITestOutputHelper helper)

        {
            this.helper = helper;
            Setup = setup;
            //helper.WriteLine($"ClientId: {Setup.Credentials.ClientId }, Secret: {Setup.Credentials.ClientSecret }");
        }

        protected HelpScoutClientFixture Setup { get; }

        public HelpScoutApiClient Client => Setup.ApiClient;
        public Faker Faker => Setup.Faker;
    }
}