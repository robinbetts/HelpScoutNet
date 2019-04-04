namespace HelpScout.Conversations
{
    public enum ConversationSortField
    {
        CreatedAt, // = "active";
        CustomerEmail, // = "closed";
        CustomerName, // = "pending";

        //These are for the searches
        MailboxId, // = "active";
        ModifiedAt, // = "active";
        Number, // = "active";
        Score, // = "active";
        Status, // = "active";
        Subject // = "active";
    }
}