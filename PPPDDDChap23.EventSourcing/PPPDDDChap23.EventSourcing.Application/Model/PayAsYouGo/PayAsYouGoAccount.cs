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
        private FreeCallAllowance _freeCallAllowance;
        private Money _credit;

        public PayAsYouGoAccount(Guid simId, Money credit)
        {
            Causes(new AccountCreated());          
        }

        public void Record(PhoneCall phoneCall, PhoneCallCosting phoneCallCosting) 
        {                         
            var numberOfMinutesCoveredByAllowance = _freeCallAllowance.MinutesWhichCanCover(phoneCall);

            var costOfCall = phoneCallCosting.DetermineCostOfCall(phoneCall, numberOfMinutesCoveredByAllowance);

            Causes(new PhoneCallCharged() { PhoneCall = phoneCall, NumberOfMinutesCoveredByAllowance = numberOfMinutesCoveredByAllowance, CostOfCall = costOfCall });
        }

        public void TopUp(Money credit)
        {
            if (PayAsYouGoInclusiveMinutesOffer.IsSatisfiedBy(credit))            
                Causes(new CreditSatisfiesFreeCallAllowanceOffer());            

            Causes(new CreditAdded() { Credit = credit});
        }

        private void Causes(IDomainEvent @event)
        {
            When((dynamic)@event);
            Changes.Add(@event);
        }

        private void When(CreditAdded creditAdded)
        {
            _credit = _credit.Add(creditAdded);
        }

        private void When(CreditSatisfiesFreeCallAllowanceOffer creditSatisfiesFreeCallAllowanceOffer)
        {
            _plan = new FreeCallAllowance();
        }

        private void When(PhoneCallCharged phoneCallCharged)
        {
            _credit = _credit.Minus(phoneCallCharged.CostOfCall);
            _freeCallAllowance.Minus(phoneCallCharged.numberOfMinutesCoveredByAllowance);
        }

        private void When(AccountCreated accountCreated)
        {
            _simId = accountCreated.SimId;
            _credit = accountCreated.credit;
        }
    }
}
