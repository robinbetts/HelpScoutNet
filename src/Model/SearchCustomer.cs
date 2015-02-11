using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public PhotoType PhotoType { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public Gender Gender { get; set; }
        public string Age { get; set; }
        public string Organization { get; set; }
        public string JobTitle { get; set; }
        public string Location { get; set; }
        public string CreatedAt { get; set; }
        public string ModifiedAt { get; set; }
    }

    public enum PhotoType
    {
        unknown,
        gravatar,
        twitter,
        facebook,
        googleprofile,
        googleplus,
        linkedin
    }

    public enum Gender
    {
        unknown,
        male,
        female,        
    }
}
