using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Request
{
    public class CreateCustomerRequest : PostOrPutRequest
    {
        public bool Imported { get; set; }
        public bool AutoReply  { get; set; }

        public override NameValueCollection ToNameValueCollection()
        {
            base.ToNameValueCollection();
            if (Imported)
                Nv.Add("imported", "true");
            if (AutoReply)
                Nv.Add("autoReply", "true");
            return Nv;
        }
    }
}
