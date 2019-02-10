using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Request.Report.User
{
    public class UserViewByRequest : UserRequest
    {
        public UserViewByRequest(int userId, DateTime? startTime, DateTime? endTime, DataResolution? viewBy = null)
            : base(userId, startTime, endTime)
        {
            ViewBy = viewBy;
        }

        /// <summary>
        /// Represents the resolution at which the data is returned
        /// </summary>
        public DataResolution? ViewBy { get; set; }

        public override NameValueCollection ToNameValueCollection()
        {
            base.ToNameValueCollection();
            if (ViewBy.HasValue)
                Nv.Add("viewBy", ((DataResolution)ViewBy).ToString().FirstCharacterToLower());
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
