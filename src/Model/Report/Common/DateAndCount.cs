using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Model.Report.Common
{
    public class DateBase
    {
        public DateTime? Date {get; set;}
    }

    public class DateAndCount : DateBase
    {

        public int Count { get; set; }
    }

    public class CustomerDateAndCount : DateBase
    {
        public int Customers { get; set; }
    }

    public class RepliesDateAndCount : DateBase
    {
        public int Replies { get; set; }
    }

    public class ResolvedDateAndCount : DateBase
    {
        public int Resolved { get; set; }
    }
}
