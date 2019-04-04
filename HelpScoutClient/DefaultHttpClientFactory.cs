using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HelpScout
{
    //resolves #1
    public sealed class DefaultHttpClientFactory : IHttpClientFactory
    {
        private static Client _client;
        private static readonly object locker = new object();

        public ISimpleHttpClient GetClient(Action<HttpClient> setupCallback = null)
        {
            lock (locker)
            {
                if (_client == null) _client = new Client();
            }

            setupCallback?.Invoke(_client.http);
            return _client;
        }

        private class Client : ISimpleHttpClient
        {
            public readonly HttpClient http = new HttpClient
            {
                BaseAddress = new Uri("https://api.helpscout.net/v2/")
            };

            public Task<HttpResponseMessage> SendRequest(HttpRequestMessage message)
            {
                return http.SendAsync(message);
            }
        }
    }
}