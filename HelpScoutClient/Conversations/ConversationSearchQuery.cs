using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace HelpScout.Conversations
{
    public class ConversationSearchQuery : ISearchQuery
    {
        public List<string> MailBoxs { get; } = new List<string>();
        public string Folder { get; set; }
        public ConversationStatus? Status { get; set; }
        public List<string> Tags { get; } = new List<string>();
        public string AssignedTo { get; set; }
        public DateTime? ModifiedSince { get; set; }
        public string Number { get; set; }
        public ConversationSortField? SortField { get; set; }
        public SortOrder? SortOrder { get; set; }
        public int Page { get; set; }
        public NameValueCollection CustomFields { get; set; }
        public string SearchExpression { get; set; }

        public NameValueCollection BuildQueryString()
        {
            var col = new NameValueCollection();

            if (MailBoxs != null && MailBoxs.Count > 0) col.Add("mailbox", MailBoxs.ToCommaSeparated());

            if (Folder.HasValue()) col.Add("folder", Folder);

            if (Status != null) col.Add("status", Status.ToString().ToLowerInvariant());

            if (Tags != null && Tags.Count > 0) col.Add("tag", Tags.ToCommaSeparated());

            if (AssignedTo.HasValue()) col.Add("assigned_to", AssignedTo);

            if (ModifiedSince != null) col.Add("modifiedSince", ModifiedSince.ToString());

            if (Number.HasValue()) col.Add("number", Number);

            if (SortField != null)
            {
                string field = null;
                switch (SortField)
                {
                    case ConversationSortField.CreatedAt:
                        field = "createdAt";
                        break;
                    case ConversationSortField.CustomerEmail:
                        field = "customerEmail";
                        break;
                    case ConversationSortField.CustomerName:
                        field = "customerName";
                        break;
                    case ConversationSortField.MailboxId:
                        field = "mailboxid";
                        break;
                    case ConversationSortField.ModifiedAt:
                        field = "modifiedAt";
                        break;
                    case ConversationSortField.Number:
                        field = "number";
                        break;
                    case ConversationSortField.Score:
                        field = "score";
                        break;
                    case ConversationSortField.Status:
                        field = "status";
                        break;
                    case ConversationSortField.Subject:
                        field = "subject";
                        break;
                }

                col.Add("sortField", field);
            }

            if (SortOrder != null)
            {
                string order = null;
                switch (SortOrder)
                {
                    case HelpScout.SortOrder.Asc:
                        order = "asc";
                        break;

                    case HelpScout.SortOrder.Desc:
                    default:
                        order = "desc";

                        break;
                }

                col.Add("sortOrder", order);
            }

            if (SearchExpression.HasValue()) col.Add("query", SearchExpression);

            if (Page > 0) col.Add("page", Page.ToString());

            if (CustomFields != null && CustomFields.Count > 0)
            {
                var allfields = CustomFields.ToPairs()
                    .Select(a => $"{a.Key}:{a.Value}").ToList();
                col.Add("customFieldsByIds", allfields.ToCommaSeparated());
            }


            return col;
        }
    }
}