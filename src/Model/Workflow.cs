using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HelpScoutNet.Model
{
    public class Workflow
    {
        public long Id { get; set; }
        public long MailboxId { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public WorkflowType Type { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public WorkflowStatus Status { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }

    public enum WorkflowStatus
    {
        active,
        inactive,
        invalid
    }

    public enum WorkflowType
    {
        automatic,
        manual
    }
}
