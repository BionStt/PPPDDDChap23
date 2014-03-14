using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.Application.Infrastructure
{
    public class StreamState
    {
        public Guid Id { get; private set; }
        public Guid AggregateId { get; private set; }
        public string AggregateType {get; private set;}
        public int NoOfEvents {get; private set;}

        private StreamState() { }

        public StreamState(Guid aggregateId, string aggregateType)
        {
            Id = Guid.NewGuid();
            AggregateType = aggregateType;
            AggregateId = aggregateId;
            NoOfEvents = 0;
        }

        public EventWrapper RegisterEvent(DomainEvent @event)
        {
            NoOfEvents += 1;

            return new EventWrapper(@event, AggregateId, AggregateType, NoOfEvents, Id);
        }
    }
}
