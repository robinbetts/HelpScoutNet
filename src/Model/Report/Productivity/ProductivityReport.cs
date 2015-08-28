using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Model.Report.Productivity
{
    public class ProductivityReport
    {
        public List<Tag> FilterTags { get; set; }
        public TimeRangeStats Current { get; set; }
        public TimeRangeStats Previous { get; set; }
        public MultipleTimeRangeStats Deltas { get; set; }
        public ProductivityStats Responsetime { get; set; }
        public ProductivityStats HandleTime { get; set; }
        public ProductivityStats FirstResponseTime { get; set; }
        public RepliesToResolveStats RepliesToResolve { get; set; }

        public class TimeRangeStats
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int TotalConversations { get; set; }
            public double ResolutionTime { get; set; }
            public double RepliesToResolve { get; set; }
            public double ResponseTime { get; set; }
            public double FirstResponseTime { get; set; }
            public int Resolve { get; set; }
            public int ResolvedOnFirstReply { get; set; }
            public int Closed { get; set; }
            public int RepliesSent { get; set; }
            public int HandleTime { get; set; }
            public double PercentResolvedOnFirstReply { get; set; }
        }
    }

    public class MultipleTimeRangeStats
    {
        public double FirstResponseTime { get; set; }
        public double HandleTime { get; set; }
        public double RepliesSent { get; set; }
        public double ResponseTime { get; set; }
        public double TotalConversations { get; set; }
        public double RepliesToResolve { get; set; }
        public double Closed { get; set; }
        public double ResolvedOnFirstReply { get; set; }
        public double ResolutionTime { get; set; }
        public double Resolved { get; set; }
    }

    public class ProductivityStats
    {
        public int Count { get; set; }
        public int PreviousCount { get; set; }
        public List<DrillDownStats> Ranges { get; set; }
    }

    public class RepliesToResolveStats
    {
        public int Count { get; set; }
        public int PreviousCount { get; set; }
        public List<RepliesToResolveDrillDownStats> ranges { get; set; }
    }

    public class DrillDownStats
    {
        public int ID { get; set; }
        public int Count { get; set; }
        public int PreviousCount { get; set; }
        public double Percent { get; set; }
        public double PreviousPercent { get; set; }
    }

    public class RepliesToResolveDrillDownStats  : DrillDownStats
    {
        public double ResolutionTime { get; set; }
    }
}
