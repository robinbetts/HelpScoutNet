using System;
using System.Collections.Generic;

namespace HelpScout.Customers
{
    public class CustomerCreateRequest
    {
        public enum CustomerPhotoType
        {
            Unknown = 1,
            Gravatar,
            Twitter,
            Facebook,
            Googleprofile,
            Googleplus,
            Linkedin
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public CustomerPhotoType PhotoType { get; set; } = CustomerPhotoType.Unknown;
        public string JobTitle { get; set; }
        public string Location { get; set; }
        public string Background { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Organization { get; set; }
        public IList<Email> Emails { get; set; }

        public class Email
        {
            public string Type { get; set; }
            public string Value { get; set; }
        }
    }
}