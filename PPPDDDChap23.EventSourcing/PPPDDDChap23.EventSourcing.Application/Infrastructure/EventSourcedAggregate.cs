using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.Application.Infrastructure
{
    public abstract class EventSourcedAggregate : Entity
    {
        protected List<DomainEvent> Changes { get; private set; }
        protected StreamState State { get; private set; }

        public EventSourcedAggregate()
        {
            Changes = new List<DomainEvent>();
        }

        public EventSourcedAggregate(EventStream eventStream)
        {
            Changes = new List<DomainEvent>();

            State = eventStream.State;
            Replay(eventStream.Events);
        }

        public EventStream GetChanges()
        {
            var eventStream = new EventStream(Changes, State);

            return eventStream; 
        }
        protected abstract void Replay(IEnumerable<DomainEvent> changes);
    }
}
