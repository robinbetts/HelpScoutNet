using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Request.Report
{
    public class CompareRequest : ReportRequest
    {
        /// <summary>
        /// Previous Start Time To Compare To
        /// </summary>
        public DateTime? PreviousStart { get; set; }

        /// <summary>
        /// Previous End Time To Compare To
        /// </summary>
        public DateTime? PreviousEnd { get; set; }

        public virtual NameValueCollection ToNameValueCollection()
        {
            if (PreviousStart.HasValue)
                Nv.Add("start", PreviousStart.Value.ToIso8601());
            if (PreviousEnd.HasValue)
                Nv.Add("end", PreviousEnd.Value.ToIso8601());
            return Nv;
        } 
    }
}
