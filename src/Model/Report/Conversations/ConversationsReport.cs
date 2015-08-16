using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Model.Report.Conversations
{
    public class ConversationsReport
    {
        public List<Tag> FilterTags { get; set; }
        public DayStats BusiestDay { get; set; }
        public int BusyTimeStart { get; set; }
        public int BusyTimeEnd { get; set; }
        public TimeRangeStats Current { get; set; }
        public TimeRangeStats Previous { get; set; }
        public MultipleTimeRangeStats Delta { get; set; }
        public TagStats Tags { get; set; }
        public CustomerStats Customer { get; set; }
        public SavedReplyStats Replies { get; set; }
        public WorkFlowStats WorkFlows { get; set; }
    }

    public class TimeRangeStats
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TotalConversations { get; set; }
        public int ConversationsCreated { get; set; }
        public int NewConversations { get; set; }
        public int Customers { get; set; }
        public int ConversationsPerDay { get; set; }
    }

    private class MultipleTimeRangeStats
    {
        public double NewConversations { get; set; }
        public double TotalConversations { get; set; }
        public double Customers { get; set; }
        public double ConversationsCreated { get; set; }
        public double ConversationsPerDay { get; set; }
    }

    private class TagStats
    {
        public int Count { get; set; }
        public List<TagStat> Top { get; set; }
    }

    private class TagStat
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int Count { get; set; }
        public int PreviousCount { get; set; }
        public double Percent { get; set; }
        public double PreviousPercent { get; set; }
        public double DeltaPercent { get; set; }
    }

    private class CustomerStats
    {
        public int Count { get; set; }
        public List<CustomerStat> Top { get; set; }
    }

    private class CustomerStat
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int Count { get; set; }
        public double PreviousCount { get; set; }
        public double Percent { get; set; }
        public double PreviousPercent { get; set; }
        public double DeltaPercent { get; set; }
    }

    private class SavedReplyStats
    {
        public int Count { get; set; }
        public List<SavedReplyStat> replies { get; set; }
    }

    private class SavedReplyStat
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int MailboxID { get; set; }
        public int Count { get; set; }
        public int PreviousCount { get; set; }
        public double Percent { get; set; }
        public double PreviousPercent { get; set; }
        public double DeltaPercent { get; set; }
    }

    private class WorkFlowStats
    {
        public int Count { get; set; }
        public List<WorkFlowStat> Top { get; set; }
    }

    private class WorkFlowStat
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int Count { get; set; }
        public int PreviousCount { get; set; }
        public double Percent { get; set; }
        public double PreviousPercent { get; set; }
        public double DeltaPercent { get; set; }
    }
}
