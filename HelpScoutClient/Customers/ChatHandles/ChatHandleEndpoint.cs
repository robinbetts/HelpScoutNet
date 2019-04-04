namespace HelpScout.Customers.ChatHandles
{
    public sealed class ChatHandleEndpoint : CustomerChildEndpointBase<ChatHandleCreateRequest, ChatHandleListItem>
    {
        public ChatHandleEndpoint(long customerId, IHelpScoutApiClient client) :
            base(customerId, "chats", client)
        {
        }
    }
}