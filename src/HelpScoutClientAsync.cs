using HelpScoutNet.Model;
using HelpScoutNet.Model.Report;
using HelpScoutNet.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

//.NET 4.5+ only

namespace HelpScoutNet
{
    public sealed class HelpScoutClientAsync
    {
        private readonly string _apiKey;
        private const string BaseUrl = "https://api.helpscout.net/v1/";
        private int timeoutSeconds = 0;

        private JsonSerializerSettings _serializerSettings
        {
            get
            {
                var serializer = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                };
                serializer.Converters.Add(new StringEnumConverter { CamelCaseText = true });

                return serializer;
            }
        }

        public HelpScoutClientAsync(string apiKey)
        {
            _apiKey = apiKey;
        }

        public HelpScoutClientAsync(string apiKey, int timeoutSeconds)
        {
            _apiKey = apiKey;
            this.timeoutSeconds = timeoutSeconds;
        }

        #region Mailboxes
        public async Task<Paged<Mailbox>> ListMailboxes(PageRequest requestArg = null)
        {
            return await Get<Paged<Mailbox>>("mailboxes.json", requestArg);
        }

        public async Task<SingleItem<Mailbox>> GetMailbox(int mailboxId, FieldRequest requestArg = null)
        {
            var singleItem = await Get<SingleItem<Mailbox>>(string.Format("mailboxes/{0}.json", mailboxId), requestArg);

            return singleItem;
        }

        public async Task<Paged<Folder>> GetFolder(int folderId, PageRequest requestArg = null)
        {
            return await Get<Paged<Folder>>(string.Format("/mailboxes/{0}/folders.json", folderId), requestArg);
        }
        #endregion

        #region Conversations

        public async Task<Paged<Conversation>> ListConversations(int mailboxId, ConversationRequest requestArg = null)
        {
            string endpoint = string.Format("mailboxes/{0}/conversations.json", mailboxId);

            return await Get<Paged<Conversation>>(endpoint, requestArg);
        }

        public async Task<Paged<Conversation>> ListConversationsInFolder(int mailboxId, int folderId, ConversationRequest requestArg = null)
        {
            string endpoint = string.Format("mailboxes/{0}/folders/{1}/conversations.json", mailboxId, folderId);

            return await Get<Paged<Conversation>>(endpoint, requestArg);
        }

        public async Task<Paged<Conversation>> ListConversationsForCustomer(int mailboxId, int customerId, ConversationRequest requestArg = null)
        {
            string endpoint = string.Format("mailboxes/{0}/customers/{1}/conversations.json", mailboxId, customerId);

            return await Get<Paged<Conversation>>(endpoint, requestArg);
        }

        public async Task<Paged<Conversation>> ListConversationsForUser(int mailboxId, int userId, FieldRequest requestArg = null)
        {
            string endpoint = string.Format("mailboxes/{0}/customers/{1}/conversations.json", mailboxId, userId);
            return await Get<Paged<Conversation>>(endpoint, requestArg);
        }

        public async Task<SingleItem<Conversation>> GetConversation(int conversationId, FieldRequest requestArg = null)
        {
            string endpoint = string.Format("conversations/{0}.json", conversationId);
            return await Get<SingleItem<Conversation>>(endpoint, requestArg);
        }

        public async Task<SingleItem<Attachment>> GetAttachement(int conversationId, FieldRequest requestArg = null)
        {
            string endpoint = string.Format("attachments/{0}/data.json", conversationId);
            return await Get<SingleItem<Attachment>>(endpoint, requestArg);
        }

        public async Task<Conversation> CreateConversation(Conversation conversation, bool imported = false, bool autoReply = false, bool reload = true)
        {
            string endpoint = "conversations.json";
            return await Post(endpoint, conversation, new CreateCustomerRequest { AutoReply = autoReply, Reload = reload, Imported = imported });
        }

        public async Task<Conversation> UpdateConversation(Conversation conversation, bool reload = true)
        {
            string endpoint = string.Format("conversations/{0}.json", conversation.Id);
            return await Put(endpoint, conversation, new PostOrPutRequest { Reload = reload });
        }

        public void DeleteConversation(int id)
        {
            string endpoint = string.Format("conversations/{0}.json", id);

            Delete(endpoint);
        }

        public void DeleteNote(int id)
        {
            string endpoint = string.Format("notes/{0}.json", id);

            Delete(endpoint);
        }

        public async Task<Thread> CreateThread(int conversationId, Thread thread, bool imported = false, bool reload = true)
        {
            string endpoint = string.Format("conversations/{0}.json", conversationId);

            return await Post(endpoint, thread, new PostOrPutRequest { Reload = reload, Imported = imported });
        }

        public async Task<string> CreateAttachment(CreateAttachmentRequest request)
        {
            string endpoint = "attachments.json";

            return await PostAttachment(endpoint, request);
        }

        public void DeleteAttachment(string id)
        {
            string endpoint = string.Format("attachments/{0}.json", id);

            Delete(endpoint);
        }

        #endregion

        #region Customers

        public async Task<Paged<Customer>> ListCustomers(CustomerRequest requestArg = null)
        {
            string endpoint = "customers.json";

            return await Get<Paged<Customer>>(endpoint, requestArg);
        }

        public async Task<Paged<Customer>> ListCustomers(int mailboxId, CustomerRequest requestArg = null)
        {
            string endpoint = string.Format("mailbox/{0}/customers.json", mailboxId);

            return await Get<Paged<Customer>>(endpoint, requestArg);
        }

        public async Task<SingleItem<Customer>> GetCustomer(int customerId, CustomerRequest requestArg = null)
        {
            string endpoint = string.Format("customers/{0}.json", customerId);

            return await Get<SingleItem<Customer>>(endpoint, requestArg);
        }

        /// <summary>
        /// Create customer
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="reload">if true return the new customer otherwise return the original customer</param>
        /// <returns>if reload is true return the new customer otherwise return the original customer</returns>
        public async Task<Customer> CreateCustomer(Customer customer, bool reload = true)
        {
            string endpoint = "customers.json";

            return await Post(endpoint, customer, new PostOrPutRequest { Reload = reload });
        }

        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="customerId">the id of the customer to update</param>
        /// <param name="customer">customer data to update</param>
        /// <param name="reload">if true return the new customer otherwise return the original customer</param>
        /// <returns>if reload is true return the new customer otherwise return the original customer</returns>
        public async Task<Customer> UpdateCustomer(int customerId, Customer customer, bool reload = true)
        {
            string endpoint = string.Format("customers/{0}.json", customerId);

            return await Put(endpoint, customer, new PostOrPutRequest { Reload = reload });
        }
        #endregion

        #region Search

        public async Task<Paged<SearchConversation>> SearchConversations(SearchRequest requestArg = null)
        {
            string endpoint = "search/conversations.json";

            return await Get<Paged<SearchConversation>>(endpoint, requestArg);
        }

        public async Task<Paged<SearchCustomer>> SearchCustomers(SearchRequest requestArg = null)
        {
            string endpoint = "search/customers.json";

            return await Get<Paged<SearchCustomer>>(endpoint, requestArg);
        }


        #endregion

        #region Tag

        public async Task<Paged<HelpScoutNet.Model.Tag>> ListTags(PageRequest requestArg = null)
        {
            string endpoint = "tags.json";

            return await Get<Paged<HelpScoutNet.Model.Tag>>(endpoint, requestArg);
        }

        #endregion

        #region Users

        public async Task<Paged<HelpScoutNet.Model.User>> ListUsers(PageRequest requestArg = null)
        {
            string endpoint = "users.json";

            return await Get<Paged<HelpScoutNet.Model.User>>(endpoint, requestArg);
        }

        public async Task<SingleItem<HelpScoutNet.Model.User>> GetUser(int userId, FieldRequest requestArg)
        {
            string endpoint = string.Format("users/{0}.json", userId);

            return await Get<SingleItem<HelpScoutNet.Model.User>>(endpoint, requestArg);
        }

        public async Task<SingleItem<HelpScoutNet.Model.User>> GetMe(FieldRequest requestArg)
        {
            string endpoint = "users/me.json";

            return await Get<SingleItem<HelpScoutNet.Model.User>>(endpoint, requestArg);
        }

        public async Task<Paged<HelpScoutNet.Model.User>> ListUserPerMailbox(int mailboxId, FieldRequest requestArg)
        {
            string endpoint = string.Format("mailboxes/{0}/users.json", mailboxId);

            return await Get<Paged<HelpScoutNet.Model.User>>(endpoint, requestArg);
        }

        #endregion

        #region Workflows

        public async Task<Paged<Workflow>> ListWorkflows(int mailboxId, FieldRequest requestArg)
        {
            string endpoint = string.Format("mailboxes/{0}/workflows.json", mailboxId);

            return await Get<Paged<Workflow>>(endpoint, requestArg);
        }

        #endregion

        #region Reports

        #region Users
        public async Task<Model.Report.User.UserReports.UserReport> GetUserOverallReport(Request.Report.User.UserRequest requestArg)
        {
            string endpoint = string.Format("reports/user.json");
            return await Get<Model.Report.User.UserReports.UserReport>(endpoint, requestArg);
        }

        public async Task<Model.Report.PagedReport<Model.Report.User.ConversationStats>> GetUserConversationHistory(Request.Report.User.UserPagedRequest requestArg)
        {
            string endpoint = string.Format("reports/user/conversation-history.json");
            return await Get<Model.Report.PagedReport<Model.Report.User.ConversationStats>>(endpoint, requestArg);
        }

        public async Task<Model.Report.Common.CustomersDatesAndCounts> GetUserCustomersHelped(Request.Report.User.UserViewByRequest requestArg)
        {
            string endpoint = string.Format("reports/user/customers-helped.json");
            return await Get<Model.Report.Common.CustomersDatesAndCounts>(endpoint, requestArg);
        }

        public async Task<Model.Report.Common.RepliesDatesAndCounts> GetUserReplies(Request.Report.User.UserViewByRequest requestArg)
        {
            string endpoint = string.Format("reports/user/replies.json");
            return await Get<Model.Report.Common.RepliesDatesAndCounts>(endpoint, requestArg);
        }

        public async Task<Model.Report.Common.ResolvedDatesAndCounts> GetUserResolved(Request.Report.User.UserViewByRequest requestArg)
        {
            string endpoint = string.Format("reports/user/resolutions.json");
            return await Get<Model.Report.Common.ResolvedDatesAndCounts>(endpoint, requestArg);
        }

        public async Task<Model.Report.User.UserHappiness> GetUserHappiness(Request.Report.User.UserRequest requestArg)
        {
            string endpoint = string.Format("reports/user/happiness.json");
            return await Get<Model.Report.User.UserHappiness>(endpoint, requestArg);
        }

        public async Task<Model.Report.PagedReport<Model.Report.Common.Rating>> GetUserRatings(Request.Report.User.UserRatingsRequest requestArg)
        {
            string endpoint = string.Format("reports/user/ratings.json");
            return await Get<PagedReport<Model.Report.Common.Rating>>(endpoint, requestArg);
        }

        private void GetUserDrillDown()
        {
            //Not Implimented
        }

        #endregion

        #region Conversations

        public async Task<Model.Report.Conversations.ConversationsReport> GetConversationsOverall(Request.Report.CompareRequest requestArg)
        {
            string endpoint = string.Format("reports/conversations.json");
            return await Get<Model.Report.Conversations.ConversationsReport>(endpoint, requestArg);
        }

        private void GetNewConversations()
        {
            //Not Implimented
        }

        private void GetConversationsDrillDown()
        {
            //Not Implimented
        }

        private void GetConversationsDrillDownByField()
        {
            // not Implimented
        }

        private void GetNewConversationsDrillDown()
        {
            // not Implimented
        }

        #endregion

        #region Team

        public async Task<Model.Report.Team.TeamReport> GetTeamOverall(Request.Report.CompareRequest requestArg)
        {
            string endpoint = string.Format("reports/team.json");
            return await Get<Model.Report.Team.TeamReport>(endpoint, requestArg);
        }

        private void GetTeamCustomersHelped()
        {
            //Not Implimented
        }

        private void GetTeamDrillDown()
        {
            //Not Implimented
        }

        #endregion

        #region Happiness

        private async Task<Model.Report.Happiness.HappinessReport> GetHappinessOverall(Request.Report.CompareRequest requestArg)
        {
            string endpoint = string.Format("reports/happiness.json");
            return await Get<Model.Report.Happiness.HappinessReport>(endpoint, requestArg);
        }

        private async Task<Paged<Model.Report.Common.Rating>> GetHappinessRatings(Request.Report.PagedRatingsRequest requestArg)
        {
            string endpoint = string.Format("reports/happiness/ratings.json");
            return await Get<Paged<Model.Report.Common.Rating>>(endpoint, requestArg);
        }

        #endregion

        #region Productivity

        private void GetProductivityOverall()
        {
            //Not Implimented
        }

        private void GetProductivityFirstResponseTime()
        {
            //Not Implimented
        }


        private void GetProductivityRepliesSent()
        {
            //Not Implimented
        }

        private void GetProductivityResolved()
        {
            //Not Implimented
        }

        private void GetProductivityResolutionTime()
        {
            //Not Implimented
        }

        private void GetProductivityResponseTime()
        {
            //Not Implimented
        }

        private void GetProductivityDrillDown()
        {
            //Not Implimented
        }

        #endregion

        #endregion


        private async Task<T> Get<T>(string endpoint, IRequest request) where T : class
        {
            string debug = BaseUrl + endpoint + ToQueryString(request);

            using (HttpClient client = InitHttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(BaseUrl + endpoint + ToQueryString(request));
                string body = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    T result = JsonConvert.DeserializeObject<T>(body, _serializerSettings);

                    return result;
                }

                var error = JsonConvert.DeserializeObject<HelpScoutError>(body);
                throw new HelpScoutApiException(error, body);
            }
        }

        private async Task<string> PostAttachment(string endpoint, CreateAttachmentRequest request)
        {
            using (HttpClient client = InitHttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonPayload = JsonConvert.SerializeObject(request, _serializerSettings);

                HttpResponseMessage response = await client.PostAsync(BaseUrl + endpoint, new StringContent(jsonPayload, Encoding.UTF8, "application/json"));
                string body = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    dynamic file = JsonConvert.DeserializeObject(body);

                    return file.item.hash;
                }

                var error = JsonConvert.DeserializeObject<HelpScoutError>(body);
                throw new HelpScoutApiException(error, body);
            }
        }

        private async Task<T> Post<T>(string endpoint, T payload, IPostOrPutRequest request)
        {
            using (HttpClient client = InitHttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var jsonPayload = JsonConvert.SerializeObject(payload, _serializerSettings);

                HttpResponseMessage response = await client.PostAsync(BaseUrl + endpoint + ToQueryString(request), new StringContent(jsonPayload, Encoding.UTF8, "application/json"));
                string body = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    if (request.Reload)
                    {
                        T result = await JsonConvert.DeserializeObject<SingleItem<T>>(body).Item;
                        return result;
                    }
                    else
                    {
                        return payload;
                    }
                }

                var error = JsonConvert.DeserializeObject<HelpScoutError>(body);
                throw new HelpScoutApiException(error, body);
            }  
        }

        private async Task<T> Put<T>(string endpoint, T payload, IPostOrPutRequest request)
        {
            using (HttpClient client = InitHttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var jsonPayload = JsonConvert.SerializeObject(payload, _serializerSettings);

                HttpResponseMessage response = await client.PutAsync(BaseUrl + endpoint + ToQueryString(request), new StringContent(jsonPayload, Encoding.UTF8, "application/json"));
                string body = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    if (request.Reload)
                    {
                        T result = await JsonConvert.DeserializeObject<SingleItem<T>>(body).Item;
                        return result;
                    }
                    else
                    {
                        return payload;
                    }
                }

                var error = JsonConvert.DeserializeObject<HelpScoutError>(body);
                throw new HelpScoutApiException(error, body);
            }
        }

        private async void Delete(string endpoint)
        {
            using (HttpClient client = InitHttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync(BaseUrl + endpoint);

                if (!response.IsSuccessStatusCode)
                {
                    string body = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<HelpScoutError>(body);
                    throw new HelpScoutApiException(error, body);
                }
            }
        }

        private HttpClient InitHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", _apiKey, "X"))));
            if (timeoutSeconds != 0)
                client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
            return client;
        }

        private static string ToQueryString(IRequest request)
        {
            NameValueCollection nvc = null;
            if (request != null)
            {
                nvc = request.ToNameValueCollection();
            }

            if (nvc == null)
                return string.Empty;

            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         select string.Format("{0}={1}", Uri.EscapeDataString(key), Uri.EscapeDataString(value)))
                .ToArray();
            return "?" + string.Join("&", array);
        }
    }
}
