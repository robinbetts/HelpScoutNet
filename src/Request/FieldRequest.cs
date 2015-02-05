using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace HelpScoutNet.Request
{
    public class FieldRequest : IRequest
    {
        public IList<string> Fields;
        protected NameValueCollection Nv = new NameValueCollection();

        public virtual NameValueCollection ToNameValueCollection()
        {                    
            if (Fields != null && Fields.Any())
                Nv.Add("fields", string.Join(",", Fields));
            return Nv;        
        }        
    }
}