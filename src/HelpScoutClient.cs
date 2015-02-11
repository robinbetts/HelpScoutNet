using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using HelpScoutNet.Model;
using HelpScoutNet.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;


namespace HelpScoutNet
{
    public sealed class HelpScoutClient
    {
        private readonly string _apiKey;
        private const string BaseUrl = "https://api.helpscout.net/v1/";

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
                serializer.Converters.Add(new StringEnumConverter {CamelCaseText = true});

                return serializer;
            }
        }

        public HelpScoutClient(string apiKey)
        {
            _apiKey = apiKey;
        }

        #region Mailboxes
        public Paged<Mailbox> ListMailboxes(PageRequest requestArg = null)
        {
            return Get<Paged<Mailbox>>("mailboxes.json", requestArg);
        }
        
        public Mailbox GetMailbox(int mailboxId, FieldRequest requestArg = null)
        {
            var singleItem = Get<SingleItem<Mailbox>>(string.Format("mailboxes/{0}.json", mailboxId), requestArg);

            return singleItem.Item;
        }

        public Paged<Folder> GetFolder(int folderId, PageRequest requestArg = null)
        {
            return Get<Paged<Folder>>(string.Format("/mailboxes/{0}/folders.json", folderId), requestArg);            
        }
        #endregion

        #region Conversations

        public Paged<Conversation> ListConservations(int mailboxId, ConversationRequest requestArg = null)
        {
            
            string endpoint = string.Format("mailboxes/{0}/conversations.json", mailboxId);

            return Get<Paged<Conversation>>(endpoint, requestArg);
        }

        public Paged<Conversation> ListConservationsInFolder(int mailboxId, int folderId, ConversationRequest requestArg = null)
        {
            string endpoint = string.Format("mailboxes/{0}/folders/{1}/conversations.json", mailboxId, folderId);

            return Get<Paged<Conversation>>(endpoint, requestArg);
        }

        public Paged<Conversation> ListConservationsForCustomer(int mailboxId, int customerId, ConversationRequest requestArg = null)
        {            
            string endpoint = string.Format("mailboxes/{0}/customers/{1}/conversations.json", mailboxId, customerId);

            return Get<Paged<Conversation>>(endpoint, requestArg);
        }

        public Paged<Conversation> ListConservationsForUser(int mailboxId, int userId, FieldRequest requestArg = null)
        {
            string endpoint = string.Format("mailboxes/{0}/customers/{1}/conversations.json", mailboxId, userId);
            return Get<Paged<Conversation>>(endpoint, requestArg);
        }

        public Conversation GetConversation(int conversationId, FieldRequest requestArg = null)
        {
            string endpoint = string.Format("conversations/{0}.json", conversationId);
            return Get<SingleItem<Conversation>>(endpoint, requestArg).Item;
        }

        public Attachment GetAttachement(int conversationId, FieldRequest requestArg = null)
        {
            string endpoint = string.Format("attachments/{0}/data.json", conversationId);
            return Get<SingleItem<Attachment>>(endpoint, requestArg).Item;
        }

        public Conversation CreateConversation(Conversation conversation, bool imported = false, bool autoReply = false, bool reload = true)
        {
            string endpoint = "conversations.json";
            return Post(endpoint, conversation, new CreateCustomerRequest{AutoReply = autoReply, Reload = reload, Imported = imported});
        }

        public Conversation UpdateConversation(Conversation conversation, bool reload = true)
        {
            string endpoint = "conversations.json";
            return Post(endpoint, conversation, new PostOrPutRequest { Reload = reload });
        }

        public Thread CreateThread(int conversationId, Thread thread, bool imported = false, bool reload = true)
        {
            string endpoint = string.Format("conversations/{0}.json",conversationId);

            return Post(endpoint, thread, new PostOrPutRequest { Reload = reload });
        }

        #endregion

        #region Customers

        public Paged<Customer> ListCustomers(CustomerRequest requestArg = null)
        {
            string endpoint = "customers.json";

            return Get<Paged<Customer>>(endpoint, requestArg);
        }

        public Paged<Customer> ListCustomers(int mailboxId, CustomerRequest requestArg = null)
        {
            string endpoint = string.Format("mailbox/{0}/customers.json", mailboxId);

            return Get<Paged<Customer>>(endpoint, requestArg);
        }

        public Customer GetCustomer(int customerId, CustomerRequest requestArg = null)
        {
            string endpoint = string.Format("customers/{0}.json", customerId);

            return Get<SingleItem<Customer>>(endpoint, requestArg).Item;
        }

        /// <summary>
        /// Create customer
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="reload">if true return the new customer otherwise return the original customer</param>
        /// <returns>if reload is true return the new customer otherwise return the original customer</returns>
        public Customer CreateCustomer(Customer customer, bool reload = true)
        {
            string endpoint = "customers.json";

            return Post(endpoint, customer, new PostOrPutRequest { Reload = reload });
        }

        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="customerId">the id of the customer to update</param>
        /// <param name="customer">customer data to update</param>
        /// <param name="reload">if true return the new customer otherwise return the original customer</param>
        /// <returns>if reload is true return the new customer otherwise return the original customer</returns>
        public Customer UpdateCustomer(int customerId, Customer customer, bool reload = true)
        {
            string endpoint = string.Format("customers/{0}.json", customerId);

            return Put(endpoint, customer, new PostOrPutRequest{ Reload = reload});
        }
        #endregion

        #region Search

        public Paged<SearchConversation> SearchConversations(SearchRequest requestArg = null)
        {
            string endpoint = "search/conversations.json";

            return Get<Paged<SearchConversation>>(endpoint, requestArg);
        }

        public Paged<SearchCustomer> SearchCustomers(SearchRequest requestArg = null)
        {
            string endpoint = "search/customers.json";

            return Get<Paged<SearchCustomer>>(endpoint, requestArg);
        }


        #endregion

        #region Tag

        public Paged<Tag> ListTags(PageRequest requestArg = null)
        {
            string endpoint = "tags.json";

            return Get<Paged<Tag>>(endpoint, requestArg);
        }

        #endregion

        #region Users

        public Paged<User> ListUsers(PageRequest requestArg = null)
        {
            string endpoint = "users.json";

            return Get<Paged<User>>(endpoint, requestArg);
        }

        public User GetUser(int userId, FieldRequest requestArg)
        {
            string endpoint = string.Format("users/{0}.json", userId);

            return Get<SingleItem<User>>(endpoint, requestArg).Item;
        }

        public User GetMe(FieldRequest requestArg)
        {
            string endpoint = "users/me.json";

            return Get<SingleItem<User>>(endpoint, requestArg).Item;
        }

        public Paged<User> ListUserPerMailbox(int mailboxId, FieldRequest requestArg)
        {
            string endpoint = string.Format("mailboxes/{0}/users.json",mailboxId);

            return Get<Paged<User>>(endpoint, requestArg);
        }

        #endregion

        #region Workflows

        public Paged<Workflow> ListWorkflows(int mailboxId, FieldRequest requestArg)
        {
            string endpoint = string.Format("mailboxes/{0}/workflows.json", mailboxId);

            return Get<Paged<Workflow>>(endpoint, requestArg);
        }


        #endregion

        private T Get<T>(string endpoint, IRequest request) where T : class
        {
            var client = InitHttpClient();
            
            HttpResponseMessage response = client.GetAsync(BaseUrl + endpoint + ToQueryString(request)).Result;
            string body = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                T result = JsonConvert.DeserializeObject<T>(body, _serializerSettings);

                return result;
            }

            var error = JsonConvert.DeserializeObject<HelpScoutError>(body);
            throw new HelpScoutApiException(error, body);                                                                 
        }

        private T Post<T>(string endpoint, T payload, IPostOrPutRequest request) 
        {
            var client = InitHttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var jsonPayload = JsonConvert.SerializeObject(payload, _serializerSettings);

            HttpResponseMessage response = client.PostAsync(BaseUrl + endpoint + ToQueryString(request), new StringContent(jsonPayload, Encoding.UTF8, "application/json")).Result;
            string body = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                if (request.Reload)
                {
                    T result = JsonConvert.DeserializeObject<SingleItem<T>>(body).Item;
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
        
        private T Put<T>(string endpoint, T payload, IPostOrPutRequest request)
        {
            var client = InitHttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var jsonPayload = JsonConvert.SerializeObject(payload, _serializerSettings);

            HttpResponseMessage response = client.PutAsync(BaseUrl + endpoint + ToQueryString(request), new StringContent(jsonPayload, Encoding.UTF8, "application/json")).Result;
            string body = response.Content.ReadAsStringAsync().Result;
            
            if (response.IsSuccessStatusCode)
            {
                if (request.Reload)
                {
                    T result = JsonConvert.DeserializeObject<SingleItem<T>>(body).Item;
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

        private HttpClient InitHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", _apiKey, "X"))));
            return client;
        }

        private static string ToQueryString(IRequest request)
        {
            NameValueCollection nvc = null;            
            if (request != null)
            {
                nvc = request.ToNameValueCollection();
            }
                
            if(nvc == null) 
                return string.Empty;

            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         select string.Format("{0}={1}", Uri.EscapeDataString(key), Uri.EscapeDataString(value)))
                .ToArray();
            return "?" + string.Join("&", array);
        }
    }

    internal class SingleItem<T>
    {
        public T Item { get; set; }
    }
}
