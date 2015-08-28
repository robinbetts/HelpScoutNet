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

    public class CustomersDatesAndCounts
    {
        public List<CustomerDateAndCount> Current { get; set; }
        public List<CustomerDateAndCount> Previous { get; set; }
    }

    public class RepliesDatesAndCounts
    {
        public List<RepliesDateAndCount> Current { get; set; }
        public List<RepliesDateAndCount> Previous { get; set; }
    }

    public class ResolvedDatesAndCounts
    {
        public List<ResolvedDateAndCount> Current { get; set; }
        public List<ResolvedDateAndCount> Previous { get; set; }
    }
}
