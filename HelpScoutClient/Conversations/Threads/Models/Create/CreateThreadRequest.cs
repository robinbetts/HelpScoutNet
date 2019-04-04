using System.Collections.Generic;

namespace HelpScout.Conversations.Threads.Models.Create
{
    public class CreateThreadRequest
    {
        public string Text { get; set; }
        public IList<Attachment> Attachments { get; set; }
    }
}