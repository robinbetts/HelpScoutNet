using System.Net.Http;
using System.Threading.Tasks;

namespace HelpScout
{
    public interface ISimpleHttpClient
    {
        Task<HttpResponseMessage> SendRequest(HttpRequestMessage message);
    }
}