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
           
            var eventStream = GetEventStreamFor(id);

            if (eventStream.Events.Count() == 0) return null;

            var payAsYouGoAccount = new PayAsYouGoAccount(GetEventStreamFor(id));

            return payAsYouGoAccount;            
        }

        private EventStream GetEventStreamFor(Guid id)
        {
            var eventStreams = (from stream in _documentSession.Query<EventStream>()
                          .Customize(x => x.WaitForNonStaleResultsAsOfNow())
                                where stream.AggregateId.Equals(id)
                                orderby stream.Version
                                select stream).ToList();

            var events = new List<DomainEvent>();
            int latestVersion = 0;

            foreach (var eventStream in eventStreams)
            {
                events.AddRange(eventStream.Events);
                latestVersion = eventStream.Version;
            }

            return new EventStream(events, latestVersion, id);
        }

        public void Add(PayAsYouGoAccount payAsYouGoAccount)
        {
            Save(payAsYouGoAccount);
        }

        public void Save(PayAsYouGoAccount payAsYouGoAccount)
        {
            var changes = payAsYouGoAccount.GetChanges();

            var versionId = LastStreamVersionPersisted(payAsYouGoAccount.Id);

            if (versionId == changes.Version)
            {
                _documentSession.Store(new EventStream(changes.Events, versionId + 1, payAsYouGoAccount.Id));
            }                        
        }

        private int LastStreamVersionPersisted(Guid id)
        {         
            var lastEventStream = (from stream in _documentSession.Query<EventStream>()
                                    .Customize(x => x.WaitForNonStaleResultsAsOfNow())
                                   where stream.AggregateId.Equals(id)
                                   orderby stream.Version
                                   select stream).LastOrDefault();

            if (lastEventStream != null)
                return lastEventStream.Version;
            else            
                return 0;            
        }
    }
}
