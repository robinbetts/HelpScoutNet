using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Model.Report.Common
{
    public class Date
    {
        public DateTime? Date{get; set;}
    }

    public class DateAndCount : Date
    {
        public int Count { get; set; }
        public int Replies { get; set; }
    }

    public class CustomerDateAndCount : Date
    {
        public int Customers { get; set; }
    }

    public class RepliesDateAndCount : Date
    {
        public int Replies { get; set; }
    }
}
