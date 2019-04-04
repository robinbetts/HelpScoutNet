using System;
using System.Collections.Generic;

namespace HelpScout.MailBoxes
{
    public class Fields
    {
        public string Href { get; set; }
    }


    public class MailboxDetail
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class Option
    {
        public long Id { get; set; }
        public int Order { get; set; }
        public string Label { get; set; }
    }

    public class MailboxFieldListitem
    {
        public long Id { get; set; }
        public bool Required { get; set; }
        public int Order { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public IList<Option> Options { get; set; }
    }

    public class MailboxFolderListitem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int UserId { get; set; }
        public int TotalCount { get; set; }
        public int ActiveCount { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}