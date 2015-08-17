using System;
using System.Collections.Specialized;


namespace HelpScoutNet.Request
{
    public class ConversationRequest : PageRequest
    {
        public string Status { get; set; }

        public DateTime? ModifiedSince { get; set; }

        public string Tag { get; set; }

        public override NameValueCollection ToNameValueCollection()
        {
            base.ToNameValueCollection();            
            
            if (!string.IsNullOrEmpty(Status))
                Nv.Add("status", Status);
            if (ModifiedSince.HasValue)
                Nv.Add("modifiedSince", ModifiedSince.Value.ToIso8601());

            return Nv;
        }
    }
}
