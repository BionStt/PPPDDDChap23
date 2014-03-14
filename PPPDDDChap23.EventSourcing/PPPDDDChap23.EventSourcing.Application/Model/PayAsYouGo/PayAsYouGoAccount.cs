using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPPDDDChap23.EventSourcing.Application.Infrastructure;

namespace PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo
{
    public class PayAsYouGoAccount : EventSourcedAggregate
    {
        private FreeCallAllowance _freeCallAllowance;
        private Money _credit;

        public PayAsYouGoAccount(Guid id, Money credit)
        {
            Causes(new AccountCreated(id, credit));          
        }

        public PayAsYouGoAccount(EventStream eventStream) : base(eventStream) { }

        protected override void Replay(IEnumerable<DomainEvent> changes)
        {
            foreach (var @event in changes)
            {
                When((dynamic)@event);
            }
        }

        public void Record(PhoneCall phoneCall, PhoneCallCosting phoneCallCosting) 
        {       
            var numberOfMinutesCoveredByAllowance = 0;
      
            if (_freeCallAllowance != null)
                numberOfMinutesCoveredByAllowance = _freeCallAllowance.MinutesWhichCanCover(phoneCall);

            var costOfCall = phoneCallCosting.DetermineCostOfCall(phoneCall, numberOfMinutesCoveredByAllowance);

            Causes(new PhoneCallCharged(this.Id, phoneCall, costOfCall, numberOfMinutesCoveredByAllowance));
        }

        public void TopUp(Money credit)
        {
            if (new PayAsYouGoInclusiveMinutesOffer().IsSatisfiedBy(credit))            
                Causes(new CreditSatisfiesFreeCallAllowanceOffer(this.Id));            

            Causes(new CreditAdded(this.Id, credit));
        }

        private void Causes(DomainEvent @event)
        {
            When((dynamic)@event);
            Changes.Add(@event);
        }

        private void When(CreditAdded creditAdded)
        {
            _credit = _credit.Add(creditAdded.Credit);
        }

        private void When(CreditSatisfiesFreeCallAllowanceOffer creditSatisfiesFreeCallAllowanceOffer)
        {
            _freeCallAllowance = new FreeCallAllowance();
        }

        private void When(PhoneCallCharged phoneCallCharged)
        {
            _credit = _credit.Subtract(phoneCallCharged.CostOfCall);

            if (_freeCallAllowance != null)
                _freeCallAllowance.Subtract(phoneCallCharged.NumberOfMinutesCoveredByAllowance);
        }

        private void When(AccountCreated accountCreated)
        {
            Id = accountCreated.Id;
            _credit = accountCreated.Credit;
        }
    }
}
