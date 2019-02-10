using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HelpScoutNet.Model
{
    public class Folder
    {
        [DefaultValue(0)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public long UserId { get; set; }
        public int TotalCount { get; set; }
        public int ActiveCount { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }

    public class Mailbox
    {
        [DefaultValue(0)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public List<Folder> Folders { get; set; }
    }
}
