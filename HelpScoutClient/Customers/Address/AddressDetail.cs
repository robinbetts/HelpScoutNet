using System.Collections.Generic;

namespace HelpScout.Customers.Address
{
    public class AddressDetail
    {
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public IList<string> Lines { get; set; }
    }
}