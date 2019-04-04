using System;
using System.Collections.Generic;
using System.Net;

namespace HelpScout
{
    public class HelpScoutException : Exception
    {
        public HelpScoutException(string message, IList<string> errors = null) : base(message)
        {
            Errors = errors ?? new List<string>();
        }

        public HttpStatusCode StatusCode { get; set; }
        public IList<string> Errors { get; }
    }
}