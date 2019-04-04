using System;
using System.Collections.Specialized;

namespace HelpScout.Customers
{
    public class CustomerListItem
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PhotoType { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Background { get; set; }
    }

    public class CustomerSearchQuery : ISearchQuery
    {
        public enum CustomerSearchField
        {
            Score = 1,
            FirstName,
            LastName,
            ModifiedAt
        }

        /// <summary>
        ///     Page Number
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        ///     Filters customers from a specific mailbox
        /// </summary>
        public long? Mailbox { get; set; }

        /// <summary>
        ///     Filters customers by first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Filters customers by last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Returns only customers that were modified after this date
        /// </summary>
        public DateTime? ModifiedSince { get; set; }

        /// <summary>
        ///     Sorts the result by specified field Default is Score
        /// </summary>
        public CustomerSearchField? SortField { get; set; }

        /// <summary>
        ///     Sort order default is desc
        /// </summary>
        public string SortOrder { get; set; }

        /// <summary>
        ///     Advanced search query
        /// </summary>
        public string Query { get; set; }

        public NameValueCollection BuildQueryString()
        {
            var nv = new NameValueCollection();
            if (Page > 0)
                nv.Add("page", Page.ToString());
            if (Mailbox != null)
                nv.Add("mailbox", Mailbox.ToString());
            if (FirstName.HasValue())
                nv.Add("firstName", FirstName);
            if (LastName.HasValue())
                nv.Add("lastName", LastName);
            if (ModifiedSince != null)
                nv.Add("modifiedSince", ModifiedSince.ToString());

            if (LastName.HasValue())
                nv.Add("lastName", LastName);

            if (SortField.HasValue)
            {
                var field = "";
                switch (SortField)
                {
                    case CustomerSearchField.FirstName:
                        field = "firstName";
                        break;
                    case CustomerSearchField.LastName:
                        field = "lastName";
                        break;
                    case CustomerSearchField.ModifiedAt:
                        field = "modifiedAt";
                        break;
                    default:
                        field = "score";
                        break;
                }

                nv.Add("sortField", field);
            }

            if (Query.HasValue()) nv.Add("query", Query);

            return nv;
        }
    }
}