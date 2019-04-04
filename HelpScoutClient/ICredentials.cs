namespace HelpScout
{
    public interface ICredentials
    {
        string ClientId { get; }
        string ClientSecret { get; }
    }
}