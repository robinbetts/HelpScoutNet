using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HelpScoutNet.Model
{
    public class Address
    {
        [DefaultValue(0)]
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
        [DefaultValue(0)]
        public int Id { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }

    public class Email
    {
        [DefaultValue(0)]
        public int Id { get; set; }
        public string Value { get; set; }
        public string Location { get; set; }
    }

    public class Phone
    {
        [DefaultValue(0)]
        public int Id { get; set; }
        public string Value { get; set; }
        public string Location { get; set; }
    }

    public class Chat
    {
        [DefaultValue(0)]
        public int Id { get; set; }
        public string Value { get; set; }                
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ChatType? Type { get; set; }
    }

    public enum ChatType
    {
        aim,
        gtalk,
        icq,
        xmpp,
        msn,
        skype,
        yahoo,
        qq,
        other
    }

    public class Website
    {
        [DefaultValue(0)]
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class Customer
    {
        [DefaultValue(0)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public PhotoType PhotoType { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string Organization { get; set; }
        public string JobTitle { get; set; }
        public string Location { get; set; }
        public string Background { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Address Address { get; set; }
        public List<SocialProfile> SocialProfiles { get; set; }
        public List<Email> Emails { get; set; }
        public List<Phone> Phones { get; set; }
        public List<Chat> Chats { get; set; }
        public List<Website> Websites { get; set; }
    }
}
