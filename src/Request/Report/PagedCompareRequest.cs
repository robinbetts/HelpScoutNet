using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Request.Report
{
    public class PagedCompareRequest : CompareRequest
    {
        public int? Page { get; set; }

        public override NameValueCollection ToNameValueCollection()
        {
            base.ToNameValueCollection();
            if (Page.HasValue)
                Nv.Add("page", Page.ToString());

            return Nv;
        }
    }
}
