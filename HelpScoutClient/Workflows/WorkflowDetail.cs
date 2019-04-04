using System;

namespace HelpScout.Workflows
{
    public class WorkflowDetail
    {
        public long Id { get; set; }
        public long MailboxId { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}