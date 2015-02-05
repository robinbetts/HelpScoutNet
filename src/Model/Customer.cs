using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HelpScoutNet.Model
{
    public class Address
    {
        public int Id { get; set; }
        public List<string> Lines { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostcalCode { get; set; }
        public string Country { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime ModifiedAt { get; set; }
    }

    public class SocialProfile
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }

    public class Email
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Location { get; set; }
    }

    public class Phone
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Location { get; set; }
    }

    public class Chat
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }

    public class Website
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoType { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string Organization { get; set; }
        public string JobTitle { get; set; }
        public string Location { get; set; }
        public string Background { get; set; }
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime CreatedAt { get; set; }
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime ModifiedAt { get; set; }
        public Address Address { get; set; }
        public List<SocialProfile> SocialProfiles { get; set; }
        public List<Email> Emails { get; set; }
        public List<Phone> Phones { get; set; }
        public List<Chat> Chats { get; set; }
        public List<Website> Websites { get; set; }
    }
}
