using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPPDDDChap23.EventSourcing.Application.Infrastructure;

namespace PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo
{
    public class PayAsYouGoAccount : EventSourcedAggregate<Guid>
    {
        private Guid _simId;
        private TopUp _lastTopUp;
        private InclusiveMinutesPlan _plan;

        public PayAsYouGoAccount(Guid simId)
        {
            _simId = simId;
        }

        public void RecordPhoneCall(callStartTime, length, phoneNumber)
        {
              // Your allowances will expire after 30 days.  
              // Calls to standard UK mobiles and landlines (01, 02, 03) within the UK.

              if (_plan.IsSatisfiedBy(phoneNumber, callStartTime))
              {
                 if (minutesThisMonth.RemainingCanCover(callStartTime, length, phoneNumber)
                 {
                    Causes(new InclusiveMinutesUsed());
                 }
             
              }
             else
              {
	          Causes(new PhoneOutSideInclusiveMinutesUsed());
              }
        }

        public void TopUp(Credit credit)
        {
	        // if credit is > $90 within 90 days you get allowance for 30 days.

            // if topping up $10 give offer X
            // if topping up $20 give offer Y
            // Use Factory to select offer.

	        Credit += credit;
        }

        // Calls 	30p a minute to any UK standard landline (beginning 01, 02, 03) or mobile
        // Voicemail 	30p a minute
        // Picture message 	36p each
        // Texts 	14p per text
        // Internet 	£1 per day for 25MB up to a maximum of 125MB per day then charged at 4p per MB thereafter
        // International 	From 5p a minute to standard landlines when you opt in to Vodafone International. Call 36888 for free from your Vodafone mobile 
    }
}
