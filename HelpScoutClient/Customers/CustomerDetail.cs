using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HelpScout.Customers
{
    public class CustomerDetail
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoType { get; set; }
        public string JobTitle { get; set; }
        public string Location { get; set; }
        public string Background { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Organization { get; set; }

        [JsonProperty("_embedded")] public CustomerEmbedded Embedded { get; set; }

        public class CustomerEmail
        {
            public long Id { get; set; }
            public string Value { get; set; }
            public string Type { get; set; }
        }

        public class CustomerWebsite
        {
            public long Id { get; set; }
            public string Value { get; set; }
        }
        public class CustomerSocialProfile
        {
            public long Id { get; set; }
            public string Value { get; set; }
        }
        public class CustomerChat
        {
            public long Id { get; set; }
            public string Value { get; set; }
            public string Type { get; set; }
        }
        public class CustomerPhone
        {
            public long Id { get; set; }
            public string Value { get; set; }
            public string Type { get; set; }
        }


        public class CustomerEmbedded
        {
            public List<CustomerEmail> Emails { get; set; }
            public List<CustomerWebsite> Websites { get; set; }
            [JsonProperty("social_profiles")]
            public List<CustomerSocialProfile> SocialProfiles { get; set; }
            public List<CustomerChat> Chats { get; set; }
            public List<CustomerPhone> Phones { get; set; }

        }
    }
}