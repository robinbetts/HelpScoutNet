using System;
using HelpScoutNet.Model;

namespace HelpScoutNet
{
    public class HelpScoutApiException : ApplicationException
    {
        public int Code { get; set; }
        public HelpScoutError HelpScoutError { get; set; }
        public string Json  { get; set; }
        
        public HelpScoutApiException(HelpScoutError helpScoutError, string jsonPayload)
            : base(helpScoutError.Error)
        {
            Code = helpScoutError.Code;
            HelpScoutError = helpScoutError;
            Json = jsonPayload;
        }

        public HelpScoutApiException(HelpScoutError helpScoutError, string jsonPayload, Exception innerException)
            : base(helpScoutError.Error, innerException)
        {
            Code = helpScoutError.Code;
            HelpScoutError = helpScoutError;
            Json = jsonPayload;
        }

    }
}
