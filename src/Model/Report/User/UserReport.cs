using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Model.Report.User.UserReports
{
    public class User
    {
        [DefaultValue(0)]
        public long Id { get; set; }
        public bool HasPhoto { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime? CreatedAt { get; set; }
        public string Name { get; set; }
        public int TotalCustomersHelped { get; set; }
        public string PhotoURL { get; set; }
    }

    public class TimeRangeStats
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime? StartDate { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime? EndDate { get; set; }
        public int TotalDays { get; set; }
        public int Resolved { get; set; }
        public int ConversationsCreated { get; set; }
        public int Closed { get; set; }
        public int TotalReplies { get; set; }
        public int ResolvedOnFirstReply { get; set; }
        public double PercentResolvedOnFirstReply { get; set; }
        public double RepliesToResolve { get; set; }
        public double HandleTime { get; set; }
        public double HappinessScore { get; set; }
        public double ResponseTime { get; set; }
        public double ResolutionTime { get; set; }
        public double RepliesPerDay { get; set; }
        public int CustomersHelped { get; set; }
        public int TotalConversations { get; set; }
        public double ConversationsPerDay { get; set; }
        public int BusiestDay { get; set; }
    }

    public class MultipleTimeRangeStats
    {
        public double TotalConversations { get; set; }
        public double CustomersHelped { get; set; }
        public double HappinessScore { get; set; }
        public double RepliesPerDay { get; set; }
        public double ResolvedOnFirstReply { get; set; }
        public double HandleTime { get; set; }
        public double ConversationsPerDay { get; set; }
        public double Resolved { get; set; }
        public double RepliesToResolve { get; set; }
        public double ActiveConversations { get; set; }
        public double TotalReplies { get; set; }
        public double Closed { get; set; }
        public double ResponseTime { get; set; }
        public double ResolutionTime { get; set; }
        public double ConversationsCreated { get; set; }
    }

    public class Tag
    {
        public string Name { get; set; }
        [DefaultValue(0)]
        public long Id { get; set; }
    }

    public class UserReport
    {
        public List<Tag> FilterTags { get; set; }
        public User User { get; set; }
        public TimeRangeStats Current { get; set; }
        public TimeRangeStats Previous { get; set; }
        public MultipleTimeRangeStats Deltas { get; set; }
    }
}
