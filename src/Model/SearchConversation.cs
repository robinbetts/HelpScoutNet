using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HelpScoutNet.Model
{
    public class SearchConversation
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public int MailboxId { get; set; }
        public string Subject { get; set; }
        public ConversationStatus Status { get; set; }
        public int ThreadCount { get; set; }
        public string Preview { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime ModifiedAt { get; set; }

    }
}
