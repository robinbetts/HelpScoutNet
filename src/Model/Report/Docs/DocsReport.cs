using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Model.Report
{
    public class DocsReport
    {
        public TimeRangeStats Current { get; set; }
        public TimeRangeStats Previous { get; set; }
        public List<SearchStats> PopularSearches { get; set; }
        public List<SearchStats> FailedSearches { get; set; }
        public List<ArticleStats> TopArticles { get; set; }
        public List<ArticleStats> TopCategories { get; set; }
        public DeltaStats Deltas { get; set; }

        public class TimeRangeStats
        {
            public int Visitors { get; set; }
            public double BrowseAction { get; set; }
            public double SentAnEmailResult { get; set; }
            public double FoundAnAnswerResult { get; set; }
            public double SearchAction { get; set; }
            public double FailedResult { get; set; }
            public double DocsViewedPerVisit { get; set; }
        }
    }

    public class SearchStats
    {
        public int Count { get; set; }
        public string ID { get; set; }
        public int Results { get; set; }
    }

    public class ArticleStats
    {
        public int Count { get; set; }
        public string ID { get; set; }
    }

    public class DeltaStats
    {
        public double FailedResult { get; set; }
        public double DocsViewedPerVisit { get; set; }
        public double FoundAnAnswerResult { get; set; }
        public double Visitors { get; set; }
        public double BrowseActions { get; set; }
        public double SearchAction { get; set; }
        public double SentAnEmailResult { get; set; }
    }
}
