using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Model.Report.Conversations
{
    public class Conversation
    {
        public long ID { get; set; }
        public int Number { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public ConversationType Type { get; set; }
        public long MailboxID { get; set; }
        public bool Attachments { get; set; }
        public string Subject { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public ConversationStatus Status { get; set; }
        public int ThreadCount { get; set; }
        public string Preview { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public List<int> CustomerIDs { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public long AssignedID { get; set; }
        public List<Tag> Tags { get; set; }
        public string AssignedName { get; set; }
    }
}
