using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HelpScoutNet.Model
{
    public class Tag
    {
        [DefaultValue(0)]
        public long Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public string Color { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
