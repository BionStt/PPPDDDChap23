using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo
{
    public class PhoneCallCharged
    {
        public Money CostOfCall { get; set; }

        public int NumberOfMinutesCoveredByAllowance { get; set; }
    }
}
