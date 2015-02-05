using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Model
{
    public class SearchCustomer
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public List<string> Emails { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoType { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string Organization { get; set; }
        public string JobTitle { get; set; }
        public string Location { get; set; }
        public string CreatedAt { get; set; }
        public string ModifiedAt { get; set; }
    }
}
