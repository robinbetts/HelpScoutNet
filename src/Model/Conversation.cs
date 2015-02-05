using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HelpScoutNet.Model
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public object Phone { get; set; }
        public string Type { get; set; }
    }

    public class MailboxStub
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Type { get; set; }
    }

    public class CreatedBy
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Type { get; set; }
    }

    public class Source
    {
        public string Type { get; set; }
        public string Via { get; set; }
    }
    
    public class Attachment
    {
        public int Id { get; set; }
        public string MimeType { get; set; }
        public string Filename { get; set; }
        public int Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Url { get; set; }
    }

    public class Thread
    {
        public int Id { get; set; }
        public Person AssignedTo { get; set; }
        public string Status { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime CreatedAt { get; set; }
        public Person CreatedBy { get; set; }
        public Source Source { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
        public Person Customer { get; set; }
        public object FromMailbox { get; set; }
        public string Body { get; set; }
        public List<string> To { get; set; }
        public List<string> Cc { get; set; }
        public List<string> Bcc { get; set; }
        public List<Attachment> Attachments { get; set; }
        public List<string> Tags { get; set; }
    }

    public class Conversation
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Folder { get; set; }
        public string IsDraft { get; set; }
        public int Number { get; set; }
        public Owner Owner { get; set; }
        public MailboxStub Mailbox { get; set; }
        public Person Customer { get; set; }
        public int ThreadCount { get; set; }
        public string Status { get; set; }
        public string Subject { get; set; }
        public string Preview { get; set; }
        public CreatedBy CreatedBy { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime ModifiedAt { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime ClosedAt { get; set; }
        public Person ClosedBy { get; set; }
        public Source Source { get; set; }
        public List<string> Cc { get; set; }
        public List<string> Bcc { get; set; }
        public List<string> Tags { get; set; }
        public List<Thread> Threads { get; set; }
    }
}
