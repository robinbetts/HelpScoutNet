using System;

namespace HelpScout.Conversations.Models.List
{
    public class CustomerWaitingSince
    {
        public DateTime Time { get; set; }
        public string Friendly { get; set; }
        public string LatestReplyFrom { get; set; }
    }
}