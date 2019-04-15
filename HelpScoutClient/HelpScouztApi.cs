using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HelpScout
{
    public class TokenManager : ITokenManager
    {
        private const string Tokenpi = "v2/oauth2/token";
        private static readonly SemaphoreSlim Gate = new SemaphoreSlim(1, 1);

        private Token token;
        private readonly ICredentials credentials;
        private readonly JsonSerializerSettings serializerSetting = SerializerSettings.Default;

        public TokenManager(ICredentials credentials, IHttpClientFactory clientFactory = null)
        {
            this.credentials = credentials;
            this.ClientFactoy = clientFactory ?? new DefaultHttpClientFactory();

        }

        public IHttpClientFactory ClientFactoy { get; }

        public async Task<Token> GetToken()
        {
            try
            {
                await Gate.WaitAsync().ConfigureAwait(false);
                return token ?? (token = (await DoHttp().ConfigureAwait(false)).Record);
            }
            finally
            {
                Gate.Release();
            }
        }

        public bool IsInitialized => token != null;

        public async Task<Token> GenerateNewToken()
        {
            token = null;
            return await GetToken().ConfigureAwait(false);
        }

        private async Task<IApiResponse<Token>> DoHttp()
        {
            var client = ClientFactoy.GetClient();

            var data = new
            {
                grant_type = "client_credentials",
                client_id = credentials.ClientId,
                client_secret = credentials.ClientSecret
            };
            var strData = JsonConvert.SerializeObject(data, serializerSetting);
            using (var req = new HttpRequestMessage(HttpMethod.Post, Tokenpi))
            {
                req.Content = new StringContent(strData, Encoding.UTF8, "application/json");
                var response = await client.SendRequest(req).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return new ApiResponse<Token>
                    {
                        ResponseHeader = response.Headers,
                        Record = !string.IsNullOrEmpty(content)
                            ? JsonConvert.DeserializeObject<Token>(content, serializerSetting)
                            : default(Token)
                    };
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var rsp = new ApiResponse<Token>
                    {
                        ResponseHeader = response.Headers
                    };

                    rsp.Errors.Add(content);
                    return rsp;
                }
            }
        }
    }


    public class ApiResponse<T> : IApiResponse<T>
    {
        public ApiResponse(IList<string> errors = null)
        {
            Errors = errors ?? new List<string>();
        }

        public bool IsSuccessful { get; set; }

        public IList<string> Errors { get; }
        public T Record { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public HttpResponseHeaders ResponseHeader { get; set; }

        object IApiResponse.Record => Record;
    }


    public class Token
    {
        [JsonProperty("token_type")] public string TokenType { get; set; }
        [JsonProperty("access_token")] public string AccessToken { get; set; }
        [JsonProperty("expires_in")] public string ExpiresIn { get; set; }
    }
}