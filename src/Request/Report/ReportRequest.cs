using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Request.Report
{
    public class ReportRequest : IRequest
    {
        public ReportRequest(DateTime? startTime, DateTime? endTime)
        {
            Start = startTime;
            End = endTime;
        }

        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        /// <summary>
        /// List of tag identifiers to filter by
        /// </summary>
        public IList<int> Tags { get; set; }

        /// <summary>
        /// List of conversation types (email, chat, or phone)
        /// </summary>
        public IList<string> Types { get; set; }

        /// <summary>
        /// List of folder identifiers to filter by
        /// </summary>
        public IList<int> Folders { get; set; }

        /// <summary>
        /// List of mailbox identifies to filter by mailboxes
        /// </summary>
        public IList<int> Mailboxes { get; set; }

        protected NameValueCollection Nv = new NameValueCollection();

        public virtual NameValueCollection ToNameValueCollection()
        {
            if (Start.HasValue)
                Nv.Add("start", Start.Value.ToIso8601());
            if (End.HasValue)
                Nv.Add("end", End.Value.ToIso8601());
            if (Tags != null && Tags.Any())
                Nv.Add("tags", string.Join(",", Tags));
            if (Mailboxes != null && Mailboxes.Any())
                Nv.Add("mailboxes", string.Join(",", Mailboxes));
            if (Types != null && Types.Any())
                Nv.Add("folders", string.Join(",", Types));
            if (Folders != null && Folders.Any())
                Nv.Add("types", string.Join(",", Folders));
            return Nv;
        } 
    }
}
