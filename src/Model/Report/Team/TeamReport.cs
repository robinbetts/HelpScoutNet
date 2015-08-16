using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Model.Report.Team
{
    public class TeamReport
    {
        public List<Tag> FilterTags { get; set; }
        public TimeRangeStats Current { get; set; }
        public TimeRangeStats Previous { get; set; }
        public MultipleTimeRangeStats Deltas { get; set; }
        public List<UserStats> Users { get; set; }

        public class TimeRangeStats
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int CustomersHelped { get; set; }
            public int Closed { get; set; }
            public int TotalReplies { get; set; }
            public int TotalUsers { get; set; }
            public int TotalDays { get; set; }
            public double RepliesPerDayPerUser { get; set; }
            public double RepliesPerDay { get; set; }
            public double ResolvedPerDay { get; set; }
        }
    }

    public class MultipleTimeRangeStats
    {
        public double RepliesPerDay { get; set; }
        public double TotalUsers { get; set; }
        public double TotalReplies { get; set; }
        public double CustomersHelped { get; set; }
        public double RepliesPerDayPerUser { get; set; }
        public double Closed { get; set; }
    }

    public class UserStats
    {
        public double HandleTime { get; set; }
        public int Replies { get; set; }
        public double HappinessScore { get; set; }
        public int CustomersHelped { get; set; }
        public double PreviousHandleTime { get; set; }
        public string Name { get; set; }
        public int PreviousCustomerHelped { get; set; }
        public double PreviousHappinessScore { get; set; }
        public string User { get; set; }
        public int PreviousReplies { get; set; }
    }
}
