using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Model.Report.User
{
    public class ConversationStats
    {
        public int Number { get; set; }
        public int Responsetime { get; set; }
        public int FirstResponseTime { get; set; }
        public int ResolveTime { get; set; }
        public int RepliesSent { get; set; }
        public long ID { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        ConversationStatus Status { get; set; }
        public List<Customer> Customers { get; set; }

        public class Customer
        {
            public long Id { get; set; }
            public string Name { get; set; }
        }
    }
}
