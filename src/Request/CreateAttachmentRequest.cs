using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Request
{
    public class CreateAttachmentRequest 
    {
        public string FileName { get; set; }

        public string MimeType { get; set; }

        public string Data { get; set; }
    }
}
