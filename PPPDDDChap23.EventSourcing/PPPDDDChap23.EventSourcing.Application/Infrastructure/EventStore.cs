using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;

namespace PPPDDDChap23.EventSourcing.Application.Infrastructure
{
    public class EventStore
    {
        private readonly IDocumentSession _documentSession;

        public EventStore(IDocumentSession documentSession)
        { 
            _documentSession = documentSession;
        }

        public void CreateNewStream(IEnumerable<DomainEvent> domainEvents, Guid aggregateId, string aggregateType)
        {
            var state = new StreamState(aggregateId, aggregateType);
            _documentSession.Store(state);

            var eventStream = new EventStream(domainEvents, state);

            AppendEventsToStream(eventStream);
        }

        public void AppendEventsToStream(EventStream eventStream)
        {
            foreach (var @event in eventStream.Events)
            {
                _documentSession.Store(eventStream.State.RegisterEvent(@event));
            }
        }

        public EventStream GetEventStreamFor(string aggregateType, Guid aggregateId)
        {
            var eventWrappers = (from stream in _documentSession.Query<EventWrapper>()
                          .Customize(x => x.WaitForNonStaleResultsAsOfNow())
                          where stream.AggregateId.Equals(aggregateId)
                          && stream.AggregateType.Equals(aggregateType)
                          orderby stream.EventNumber
                          select stream).ToList();

            if (eventWrappers.Count() == 0) return null;

            var stateId = eventWrappers.First().StreamStateId;

            var state = _documentSession.Load<StreamState>("StreamStates/" + stateId);

            var events = new List<DomainEvent>();

            foreach (var @event in eventWrappers)
            {
                events.Add(@event.Event);
            }

            return new EventStream(events, state);
        }
    }
}
