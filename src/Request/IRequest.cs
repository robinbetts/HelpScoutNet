using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Request
{
    public interface IRequest
    {
        NameValueCollection ToNameValueCollection();
    }
}
