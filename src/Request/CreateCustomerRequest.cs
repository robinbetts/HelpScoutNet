using System.Collections.Specialized;

namespace HelpScoutNet.Request
{
    public class CreateCustomerRequest : PostOrPutRequest
    {        
        public bool AutoReply  { get; set; }

        public override NameValueCollection ToNameValueCollection()
        {
            base.ToNameValueCollection();            
            if (AutoReply)
                Nv.Add("autoReply", "true");
            return Nv;
        }
    }
}
