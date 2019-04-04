using System;
using System.Net.Http;

namespace HelpScout
{
    public interface IHttpClientFactory
    {
        ISimpleHttpClient GetClient(Action<HttpClient> clientSetup = null);
    }
}