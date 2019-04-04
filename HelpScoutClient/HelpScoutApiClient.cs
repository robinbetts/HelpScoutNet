using System.Threading.Tasks;
using HelpScout.Conversations;
using HelpScout.Customers;
using HelpScout.MailBoxes;
using HelpScout.Users;
using HelpScout.Workflows;

namespace HelpScout
{
    public class HelpScoutApiClient : IHelpScoutApiClient
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ITokenManager tokenManager;

        public HelpScoutApiClient(string clientId, string clientSecret) :
            this(new ApiCredentials(clientId, clientSecret))
        {
        }

        public HelpScoutApiClient(ICredentials credentials,IHttpClientFactory factory=null)
        {
            tokenManager = new TokenManager(credentials);
            clientFactory = factory??new DefaultHttpClientFactory();
        }


        public ConversationEndpoint Conversations => new ConversationEndpoint(this);
        public CustomerEndpoint Customers => new CustomerEndpoint(this);
        public MailboxEndpoint Mailboxes => new MailboxEndpoint(this);
        public UsersEndpoint Users => new UsersEndpoint(this);
        public WorkflowEndpoint Workflows => new WorkflowEndpoint(this);
        ITokenManager IHelpScoutApiClient.TokenManager => tokenManager;
        IHttpClientFactory IHelpScoutApiClient.ClientFactory => clientFactory;

        public Task<Token> GetToken(bool force = false)
        {
            return force ? ((TokenManager) tokenManager).GenerateNewToken() : tokenManager.GetToken();
        }
    }
}