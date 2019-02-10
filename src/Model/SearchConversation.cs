using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HelpScoutNet.Model
{
    public class SearchConversation
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public long MailboxId { get; set; }
        public string Subject { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public ConversationStatus Status { get; set; }
        public int ThreadCount { get; set; }
        public string Preview { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime? ModifiedAt { get; set; }

    }
}
