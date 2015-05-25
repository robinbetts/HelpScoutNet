using System;
using System.Collections.Specialized;

namespace HelpScoutNet.Request
{
    public class CustomerRequest : PageRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        
        public DateTime? ModifiedSince { get; set; }

        public override NameValueCollection ToNameValueCollection()
        {
            base.ToNameValueCollection();
            if (!string.IsNullOrEmpty(FirstName))
                Nv.Add("firstName", FirstName);
            if (!string.IsNullOrEmpty(LastName))
                Nv.Add("lastName", LastName);
            if (!string.IsNullOrEmpty(Email))
                Nv.Add("email", Email);
            if (!ModifiedSince.HasValue)
                Nv.Add("modifiedSince", ModifiedSince.Value.ToIso8601());
            
            return Nv;
        }
    }
}
