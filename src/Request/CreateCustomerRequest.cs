using System.Collections.Specialized;

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
