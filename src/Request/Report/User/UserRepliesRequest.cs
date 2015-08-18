using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Request.Report.User
{
    public class UserRepliesRequest : CompareRequest
    {
        public UserRepliesRequest(int userID, DateTime? startTime, DateTime? endTime)
        {
            Start = startTime;
            End = endTime;
            User = userID;
        }

        public int User { get; set; }

        /// <summary>
        /// List of folder identifiers to filter by
        /// </summary>
        public IList<int> Folders { get; set; }

        /// <summary>
        /// Represents the resolution at which the data is returned
        /// </summary>
        public DataResolution? ViewBy { get; set; }

        public override NameValueCollection ToNameValueCollection()
        {
            base.ToNameValueCollection();
            if (Folders != null && Folders.Any())
                Nv.Add("types", string.Join(",", Folders));
            if (ViewBy.HasValue)
                Nv.Add("viewBy", ((DataResolution)ViewBy).ToString().FirstCharacterToLower());
            if (User >= 0)
                Nv.Add("user", User.ToString());
            return Nv;
        }


        public enum DataResolution
        {
            day,
            week,
            month
        }
    }
}
