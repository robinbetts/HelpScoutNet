using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Request.Report.User
{
    public class UserConversationHistoryRequest : PageRequest
    {
        public UserConversationHistoryRequest(int userID, DateTime? startTime, DateTime? endTime)
        {
            Start = startTime;
            End = endTime;
            User = userID;
        }

        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int User { get; set; }

        /// <summary>
        /// Previous Start Time To Compare To
        /// </summary>
        public DateTime? PreviousStart { get; set; }

        /// <summary>
        /// Previous End Time To Compare To
        /// </summary>
        public DateTime? PreviousEnd { get; set; }

        /// <summary>
        /// List of mailbox identifies to filter by mailboxes
        /// </summary>
        public IList<int> Mailboxes { get; set; }

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

        public override NameValueCollection ToNameValueCollection()
        {
            base.ToNameValueCollection();
            if (Start.HasValue)
                Nv.Add("start", Start.Value.ToIso8601());
            if (End.HasValue)
                Nv.Add("end", End.Value.ToIso8601());
            if (PreviousStart.HasValue)
                Nv.Add("previousStart", PreviousStart.Value.ToIso8601());
            if (PreviousEnd.HasValue)
                Nv.Add("previousEnd", PreviousEnd.Value.ToIso8601());
            if (Mailboxes != null && Mailboxes.Any())
                Nv.Add("mailboxes", string.Join(",", Mailboxes));
            if (Tags != null && Tags.Any())
                Nv.Add("tags", string.Join(",", Tags));
            if (Types != null && Types.Any())
                Nv.Add("folders", string.Join(",", Types));
            if (Folders != null && Folders.Any())
                Nv.Add("types", string.Join(",", Folders));
            if (User >= 0)
                Nv.Add("user", User.ToString());
            return Nv;
        }
    }
}
