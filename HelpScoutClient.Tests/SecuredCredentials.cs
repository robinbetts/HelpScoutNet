using System;

namespace HelpScout.Tests
{
    public class SecuredCredentials : ICredentials
    {
        public string ClientId => Environment.GetEnvironmentVariable("HelpScout:ClientId");
        public string ClientSecret => Environment.GetEnvironmentVariable("HelpScout:ClientSecret");

    }
}