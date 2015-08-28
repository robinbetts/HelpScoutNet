using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Request.Report.User
{
    public class UserRatingsRequest : UserPagedRequest
    {
        public UserRatingsRequest(int userID, DateTime? startTime, DateTime? endTime, Ratings? rating)
            : base(userID, startTime, endTime)
        {
            Rating = rating;
        }

        public Ratings? Rating { get; set; }
        public SortField? SortField { get; set; }
        public SortOrder? SortOrder { get; set; }

        public override NameValueCollection ToNameValueCollection()
        {
            base.ToNameValueCollection();
            if (Rating.HasValue)
                Nv.Add("rating", ((int)Rating).ToString());
            if (SortField.HasValue)
                Nv.Add("sortField", ((SortField)SortField).ToString().FirstCharacterToLower());
            if (SortOrder.HasValue)
                Nv.Add("sortOrder", ((SortOrder)SortOrder).ToString().FirstCharacterToLower());
            return Nv;
        }
    }

    public enum Ratings
    {
        All = 0,
        Great = 1,
        Okay = 2,
        Not_Good = 3
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
