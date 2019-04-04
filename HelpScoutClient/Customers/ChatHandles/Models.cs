namespace HelpScout.Customers.ChatHandles
{
    public class ChatHandleCreateRequest
    {
        public string Value { get; set; }
        public ChatHandleType Type { get; set; }
    }

    public enum ChatHandleType
    {
        Aim = 1,
        Gtalk,
        Icq,
        Msn,
        Other,
        Qq,
        Skype,
        Xmpp,
        Yahoo
    }

    public class ChatHandleListItem
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }
}