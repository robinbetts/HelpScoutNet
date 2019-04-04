using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HelpScout.Conversations.Threads.Models.List
{
    public class Action
    {
        public string Type { get; set; }
        public string Text { get; set; }
    }

    public class Source
    {
        public string Type { get; set; }
        public string Via { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
    }

    public class CreatedBy
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
    }

    public class AssignedTo
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public string Email { get; set; }
    }

    public class Data
    {
        public string Href { get; set; }
    }

    public class Self
    {
        public string Href { get; set; }
    }

    public class Links
    {
        public Data Data { get; set; }
        public Self Self { get; set; }
    }

    public class Attachment
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public string MimeType { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Size { get; set; }
        public Links Links { get; set; }
    }

    public class E1Embedded
    {
        public IList<Attachment> Attachments { get; set; }
    }

    public class Thread
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string State { get; set; }
        public Action Action { get; set; }
        public string Body { get; set; }
        public Source Source { get; set; }
        public Customer Customer { get; set; }
        public CreatedBy CreatedBy { get; set; }
        public AssignedTo AssignedTo { get; set; }
        public int SavedReplyId { get; set; }
        public IList<string> To { get; set; }
        public IList<string> Cc { get; set; }
        public IList<string> Bcc { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? OpenedAt { get; set; }
        [JsonProperty("_embedded")] public E1Embedded E1Embedded { get; set; }
        public Links Links { get; set; }
    }

    public class EEmbedded
    {
        public IList<Thread> Threads { get; set; }
    }

    public class Page
    {
        public int Size { get; set; }
        public int TotalElements { get; set; }
        public int TotalPages { get; set; }
        public int Number { get; set; }
    }

    public class ThreadDetail
    {
        [JsonProperty("_embedded")] public EEmbedded EEmbedded { get; set; }
        public Links Links { get; set; }
        public Page Page { get; set; }
    }
}