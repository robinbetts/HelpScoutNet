using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HelpScout
{
    public abstract class EndpointBase
    {
        protected readonly string baseUrl = "https://api.helpscout.net/v2/";
        protected readonly IHelpScoutApiClient Client;
        protected JsonSerializerSettings SerializerSetting = SerializerSettings.Default;


        protected EndpointBase(IHelpScoutApiClient client)
        {
            Client = client;
        }

        protected Task<IApiResponse<T1>> Patch<T1>(string fragment, object data)
        {
            return RequestSingle<T1>(fragment, data, new HttpMethod("PATCH"));
        }

        protected async Task<IApiResponse<T1>> RequestSingle<T1>(string fragment, object data = null,
            HttpMethod method = null)
        {
            var client = await CreateClient().ConfigureAwait(false);
            method = method ?? HttpMethod.Post;

            var strData = data != null ? JsonConvert.SerializeObject(data, SerializerSetting) : null;

            using (var req = new HttpRequestMessage(method, fragment))
            {
                req.Content = data != null
                    ? new StringContent(strData, Encoding.UTF8, "application/json")
                    : null;
                var response = await client.SendRequest(req).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    //deserialize partial fragment ie, what is required only
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return new ApiResponse<T1>
                    {
                        ResponseHeader = response.Headers,
                        StatusCode = response.StatusCode,
                        IsSuccessful = true,
                        Record = !string.IsNullOrEmpty(content)
                            ? JObject.Parse(content).ToObject<T1>(JsonSerializer.Create(SerializerSetting))
                            : default(T1)
                    };
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var rsp = new ApiResponse<T1>
                    {
                        ResponseHeader = response.Headers,
                        IsSuccessful = false,
                        StatusCode = response.StatusCode
                    };
                    rsp.Errors.Add(content);
                    return rsp;
                }
            }
        }

        protected async Task<IApiResponse<PagedResult<TListItem>>> GetCollection<TListItem, TSearchQuery>(
            TSearchQuery criteria)
        {
            var fragment = GetEndPointName();
            if (criteria != null)
                if (criteria is ISearchQuery searchCriteria)
                {
                    var query = searchCriteria.BuildQueryString().ConvertToQueryString();
                    if (query.HasValue()) fragment = $"{fragment}?{query}";
                }

            return await GetCollectionInternal<TListItem, TSearchQuery>(fragment, criteria).ConfigureAwait(false);
        }

        protected async Task<IApiResponse<PagedResult<TListItem>>> GetCollectionInternal<TListItem, TCriteria>(
            string fragment, TCriteria criteria)
        {
            //remove query string from the fragments if any
            var index = fragment.IndexOf("?", StringComparison.Ordinal);
            fragment = index > -1 ? fragment.Substring(0, index) : fragment;


            //some endpoints are the child endpoint, to get the node name get the last word after the last slash
            var fragments = fragment.Split(new[] {"/"}, StringSplitOptions.RemoveEmptyEntries);
            var collectionNodeName = fragments.Last();
            var client = await CreateClient().ConfigureAwait(false);
            {
                if (criteria != null)
                    if (criteria is ISearchQuery searchQuery)
                    {
                        var query = searchQuery.BuildQueryString().ConvertToQueryString();
                        if (query.HasValue()) fragment = $"{fragment}?{query}";
                    }

                var msg = new HttpRequestMessage(HttpMethod.Get, fragment);
                var response = await client.SendRequest(msg).ConfigureAwait(false);
                var result = new PagedResult<TListItem>();
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var parsed = JObject.Parse(content);
                    var collectionNode = parsed.SelectToken("_embedded")?.SelectToken(collectionNodeName);
                    if (collectionNode != null)
                        result.Items =
                            collectionNode.ToObject<List<TListItem>>(JsonSerializer.Create(SerializerSetting));

                    var pageNode = parsed.SelectToken("page");
                    if (pageNode != null)
                    {
                        result.Size = pageNode.SelectToken("size")?.Value<int>() ?? 0;
                        result.TotalPages = pageNode.SelectToken("totalPages")?.Value<int>() ?? 0;
                        result.Number = pageNode.SelectToken("number")?.Value<int>() ?? 0;
                        result.TotalElements = pageNode.SelectToken("totalElements")?.Value<int>() ?? 0;
                    }

                    return new ApiResponse<PagedResult<TListItem>>
                    {
                        ResponseHeader = response.Headers,
                        IsSuccessful = true,
                        Record = result,
                        StatusCode = response.StatusCode
                    };
                }

                return new ApiResponse<PagedResult<TListItem>>
                {
                    ResponseHeader = response.Headers,
                    IsSuccessful = false,
                    Record = null,
                    StatusCode = response.StatusCode
                };
            }
        }

        protected virtual async Task<ISimpleHttpClient> CreateClient()
        {
            if (!Client.TokenManager.IsInitialized)
                throw new Exception("Token manager is not initialized. Initialize this by calling GetToken() method");
            var token = await Client.TokenManager.GetToken().ConfigureAwait(false);


            return Client.ClientFactory.GetClient(client =>
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token.AccessToken);
            });
        }


        protected abstract string GetEndPointName();

        protected async Task<IApiResponse<TDetail>> GetResource<TDetail>(string res)
        {
            return await RequestSingle<TDetail>(GetIdfiedEndpoint(res), method: HttpMethod.Get).ConfigureAwait(false);
        }

        protected async Task<IApiResponse> DeleteResource(string res)
        {
            return await RequestSingle<object>(GetIdfiedEndpoint(res), null, HttpMethod.Delete).ConfigureAwait(false);
        }

        protected async Task<IApiResponse> CreateResource<TCreateRequest>(TCreateRequest data)
        {
            return await RequestSingle<object>($"{GetEndPointName()}", data, HttpMethod.Post).ConfigureAwait(false);
        }

        protected async Task<IApiResponse>
            UpdateResource<TCreateRequest>(string resourceIdentifier, TCreateRequest data)
        {
            return await RequestSingle<object>(GetIdfiedEndpoint(resourceIdentifier), data, HttpMethod.Put).ConfigureAwait(false);
        }


        private string GetIdfiedEndpoint(string lastPart)
        {
            var fragment = GetEndPointName();
            if (!string.IsNullOrEmpty(lastPart))
                fragment += $"/{lastPart}";
            return fragment;
        }
    }
}