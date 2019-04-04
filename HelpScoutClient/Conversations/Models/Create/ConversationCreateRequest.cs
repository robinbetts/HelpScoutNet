using System;
using System.Collections.Generic;

namespace HelpScout.Conversations.Models.Create
{
    public class ConversationCreateRequest
    {
        public string Subject { get; set; }
        public bool? AutoReply { get; set; }
        public bool? Imported { get; set; }
        public ConversationType Type { get; set; }
        public long? AssignTo { get; set; }
        public long MailboxId { get; set; }
        public ConversationStatus Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Customer Customer { get; set; }
        public IList<ThreadCreateRequest> Threads { get; set; }
        public IList<string> Tags { get; set; }
        public IList<string> Fields { get; set; }
        public long? User { get; set; }
        public DateTime? ClosedAt { get; set; }
    }
}