using System;

namespace HelpScout.Tags
{
    public class TagDetail
    {
        public long Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int TicketCount { get; set; }
    }
}