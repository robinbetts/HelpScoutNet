using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Request.Report.User
{
    public class UserRequest : CompareRequest
    {
        public UserRequest(int userID, DateTime? startTime, DateTime? endTime)
            : base(startTime, endTime)
        {
            User = userID;
        }


        public int User { get; set; }

        public override NameValueCollection ToNameValueCollection()
        {
            base.ToNameValueCollection();
            if (Folders != null && Folders.Any())
                Nv.Add("types", string.Join(",", Folders));
            if (User >= 0)
                Nv.Add("user", User.ToString());
            return Nv;
        }
    }
}
