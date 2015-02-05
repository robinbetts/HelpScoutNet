using System.Collections.Specialized;

namespace HelpScoutNet.Request
{
    public class PageRequest : FieldRequest
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