using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet
{
    /// <summary>
    /// Easily check if the webhook is send from Helpscout
    /// </summary>
    public sealed class HelpScoutWebhookHelper
    {
        private readonly string _secret;

        public HelpScoutWebhookHelper(string secret)
        {
            _secret = secret;
        }

        public bool IsFromHelpScout(string data, string signature)
        {            
            var encoding = new UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(_secret);
            byte[] messageBytes = encoding.GetBytes(data);
            bool match;
            using (var hmacsha1 = new HMACSHA1(keyByte))
            {
                byte[] hashmessage = hmacsha1.ComputeHash(messageBytes);

                string computed = Convert.ToBase64String(hashmessage);
                match = computed == signature;
            }

            return match;
        }
    }
}
