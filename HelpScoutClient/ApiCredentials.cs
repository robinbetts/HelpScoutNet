namespace HelpScout
{
    internal class ApiCredentials : ICredentials
    {
        public ApiCredentials(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public string ClientId { get; }
        public string ClientSecret { get; }
    }
}