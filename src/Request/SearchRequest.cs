using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Request
{
    public class SearchRequest : PageRequest
    {
        public string Query { get; set; }

        public SortField? SortField { get; set; }

        public SortOrder? SortOrder { get; set; }

        public override NameValueCollection ToNameValueCollection()
        {
            base.ToNameValueCollection();
            if (!string.IsNullOrEmpty(Query))
                Nv.Add("query", Query);

            if (SortField.HasValue)
                Nv.Add("sortField", ((SortField)SortField).ToString().FirstCharacterToLower());

            if (SortField.HasValue)
                Nv.Add("SortOrder", ((SortOrder)SortOrder).ToString().FirstCharacterToLower());

            return Nv;
        }
    }

    public enum SortOrder
    {
        Asc,
        Desc
    }

    public enum SortField
    {
        CustomerEmail,
        CustomerName,
        Mailboxid,
        ModifiedAt,
        Number,
        Score,
        Status,
        Subject
    }
}
