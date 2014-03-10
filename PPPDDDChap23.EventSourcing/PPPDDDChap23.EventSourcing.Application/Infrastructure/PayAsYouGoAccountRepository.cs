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
        private readonly IDocumentSession _documentSession;

        public PayAsYouGoAccountRepository(IDocumentSession documentSession)
        { 
            _documentSession = documentSession;
        }

        public PayAsYouGoAccount FindBy(Guid id)
        {           
            var events = (from container in _documentSession.Query<EventRecord>()
                          .Customize(x => x.WaitForNonStaleResultsAsOfNow())
                            where container.Event.Id.Equals(id)
                            select container.Event).ToList();

            // NB: This might need to be in a certain order

            if (events.Count == 0) return null;

            var payAsYouGoAccount = new PayAsYouGoAccount(events);

            return payAsYouGoAccount;            
        }

        public void Add(PayAsYouGoAccount payAsYouGoAccount)
        {
            Save(payAsYouGoAccount);
        }

        public void Save(PayAsYouGoAccount payAsYouGoAccount)
        {
            var changes = payAsYouGoAccount.GetChanges();
                     
            foreach (var @event in changes)
            {
                _documentSession.Store(new EventRecord(@event));
            }               
            
        }
    }
}
