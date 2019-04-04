namespace HelpScout.Conversations.Models.Detail
{
    public class Links
    {
        public Assignee Assignee { get; set; }
        public ClosedBy ClosedBy { get; set; }
        public CreatedByCustomer CreatedByCustomer { get; set; }
        public Mailbox Mailbox { get; set; }
        public PrimaryCustomer PrimaryCustomer { get; set; }
        public Self Self { get; set; }
        public Threads Threads { get; set; }
        public Web Web { get; set; }
    }
}