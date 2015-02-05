using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HelpScoutNet.Model
{
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int UserId { get; set; }
        public int TotalCount { get; set; }
        public int ActiveCount { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime ModifiedAt { get; set; }
    }

    public class Mailbox
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Email { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime ModifiedAt { get; set; }
        public List<Folder> Folders { get; set; }
    }
}
