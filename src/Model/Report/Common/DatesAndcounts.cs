using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpScoutNet.Model.Report.Common
{
    public class DatesAndCounts
    {
        public List<DateAndCount> Current { get; set; }
        public List<DateAndCount> Previous { get; set; }
    }

    public class CustomersDatesAndAcounts
    {
        public List<CustomerDateAndCount> Current { get; set; }
        public List<CustomerDateAndCount> Previous { get; set; }
    }
}
