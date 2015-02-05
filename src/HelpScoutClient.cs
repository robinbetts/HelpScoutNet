using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using HelpScoutNet.Model;
using HelpScoutNet.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ErrorEventArgs = Newtonsoft.Json.Serialization.ErrorEventArgs;

namespace HelpScoutNet
{
    public sealed class HelpScoutClient
    {
        private readonly string _apiKey;
        private const string BaseUrl = "https://api.helpscout.net/v1/";

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
            string endpoint = string.Format("customers{0}.json", customerId);

            return Get<SingleItem<Customer>>(endpoint, requestArg).Item;
        }




        #endregion

        #region Search

        public Paged<SearchConversation> ListSearchConversations(SearchRequest requestArg = null)
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
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", _apiKey, "X"))));

            string queryString = "";
            if (request != null)
                queryString = ToQueryStringFormat(request.ToNameValueCollection());

            HttpResponseMessage response = client.GetAsync(BaseUrl + endpoint + queryString).Result;

            response.EnsureSuccessStatusCode();

            string message = response.Content.ReadAsStringAsync().Result;
            
            T result = JsonConvert.DeserializeObject<T>(message);
            
            return result;
        }

        private static string ToQueryStringFormat(NameValueCollection nvc)
        {
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
