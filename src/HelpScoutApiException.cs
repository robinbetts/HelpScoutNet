using System;
using HelpScoutNet.Model;

namespace HelpScoutNet
{
    public class HelpScoutApiException : ApplicationException
    {
        public int Code { get; set; }
        public HelpScoutError HelpScoutError { get; set; }
        
        public HelpScoutApiException(HelpScoutError helpScoutError)
            : base(helpScoutError.Error)
        {
            Code = helpScoutError.Code;
            HelpScoutError = helpScoutError;
        }

        public HelpScoutApiException(HelpScoutError helpScoutError, Exception innerException)
            : base(helpScoutError.Error, innerException)
        {
            Code = helpScoutError.Code;
            HelpScoutError = helpScoutError;
        }

    }
}
