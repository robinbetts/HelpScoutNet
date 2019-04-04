namespace HelpScout
{
    public interface IHelpScoutApiClient
    {
        ITokenManager TokenManager { get; }
        IHttpClientFactory ClientFactory { get; }
    }
}