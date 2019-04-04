using System.Threading.Tasks;

namespace HelpScout
{
    public interface ITokenManager
    {
        bool IsInitialized { get; }
        Task<Token> GetToken();
    }
}