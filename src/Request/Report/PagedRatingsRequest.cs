using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Request.Report
{
    public class PagedRatingsRequest : ReportRequest
    {
        public PagedRatingsRequest(DateTime? startTime, DateTime? endTime, int rating)
                : base(startTime, endTime)
        {

        }

        public int? Rating { get; set; }
        public int? Page { get; set; }
        /// <summary>
        /// Valid input: number, modifiedAt, rating
        /// </summary>
        public string SortField { get; set; }
        /// <summary>
        /// Valid input: ASC or DESC
        /// </summary>
        public string SortOrder { get; set; }

        public override NameValueCollection ToNameValueCollection()
        {
            base.ToNameValueCollection();
            if (Page.HasValue)
                Nv.Add("page", Page.ToString());
            if (Rating.HasValue)
                Nv.Add("rating", Rating.ToString());
            if (!string.IsNullOrEmpty(SortField))
                Nv.Add("sortfield", SortField.ToString());
            if (!string.IsNullOrEmpty(SortOrder))
                Nv.Add("sortorder", SortOrder.ToString());
            return Nv;
        }
    }
}
