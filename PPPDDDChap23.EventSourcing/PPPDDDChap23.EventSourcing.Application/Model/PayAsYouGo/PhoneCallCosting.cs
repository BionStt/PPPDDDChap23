using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo
{
    public class PhoneCallCosting
    {
        // Calls 	30p a minute to any UK standard landline (beginning 01, 02, 03) or mobile
        // Voicemail 	30p a minute
        // Picture message 	36p each
        // Texts 	14p per text
        // Internet 	£1 per day for 25MB up to a maximum of 125MB per day then charged at 4p per MB thereafter
        // International 	From 5p a minute to standard landlines when you opt in to Vodafone International. Call 36888 for free from your Vodafone mobile 

        public Money DetermineCostOfCall(PhoneCall phoneCall, int numberOfMinutesCoveredByAllowance)
        {
            // if number starts +44 its UK.

            return new Money();
        }
    }
}
