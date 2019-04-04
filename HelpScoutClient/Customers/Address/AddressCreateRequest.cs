using System.Collections.Generic;
using Newtonsoft.Json;

namespace HelpScout.Customers.Address
{
    public class AddressCreateRequest
    {
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        [JsonProperty("country")] public string CountryCode { get; set; }

        public IList<string> Lines { get; set; }
    }
}