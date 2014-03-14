using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo;

namespace PPPDDDChap23.EventSourcing.Application.Infrastructure
{
    public class PayAsYouGoAccountRepository : IPayAsYouGoAccountRepository
    {
        private readonly EventStore _eventStore;

        public PayAsYouGoAccountRepository(EventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public PayAsYouGoAccount FindBy(Guid id)
        {
            var eventStream = _eventStore.GetEventStreamFor(typeof(PayAsYouGoAccount).Name, id);

            var payAsYouGoAccount = new PayAsYouGoAccount(eventStream);

            return payAsYouGoAccount;            
        }

        public void Add(PayAsYouGoAccount payAsYouGoAccount)
        {
            _eventStore.CreateNewStream(payAsYouGoAccount.GetChanges().Events, payAsYouGoAccount.Id, typeof(PayAsYouGoAccount).Name);
        }

        public void Save(PayAsYouGoAccount payAsYouGoAccount)
        {
            _eventStore.AppendEventsToStream(payAsYouGoAccount.GetChanges());                      
        }

    }
}
