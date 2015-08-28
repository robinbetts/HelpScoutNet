using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Model.Report.Happiness
{
    public class HappinessReport
    {
        public List<Tag> FilterTags { get; set; }
        public TimeRangeStats Current { get; set; }
        public TimeRangeStats Previous { get; set; }
        public MultipleTimeRangeStats Deltas { get; set; }

        public class TimeRangeStats
        {
            public double Okay { get; set; }
            public int NotGoodCount { get; set; }
            public int TotalCustomers { get; set; }
            public double HappinessScore { get; set; }
            public int TotalCustomersWithRatings { get; set; }
            public int RatingsCount { get; set; }
            public double RatingsPercent { get; set; }
            public double Notgood { get; set; }
            public double Great { get; set; }
            public int GreatCount { get; set; }
            public int OkayCount { get; set; }
        }
    }

    public class MultipleTimeRangeStats
    {
        public double Okay { get; set; }
        public double NotGoodCount { get; set; }
        public double HappinessScore { get; set; }
        public double NotGood { get; set; }
        public double Great { get; set; }
        public double GreatCount { get; set; }
        public double OkayCount { get; set; }
    }
}
