namespace HelpScout.Customers.Phones
{
    public class PhoneCreateRequest : ICreateRequest
    {
        public PhoneType Type { get; set; }
        public string Value { get; set; }
    }
}