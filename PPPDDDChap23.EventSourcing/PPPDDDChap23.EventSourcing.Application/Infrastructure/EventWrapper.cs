using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.Application.Infrastructure
{
    public class EventWrapper
    {
        public DomainEvent Event { get; private set; }
        public Guid StreamStateId { get; private set; }
        public string AggregateType {get; private set;}
        public Guid AggregateId { get; private set; }
        public int EventNumber { get; private set; }

        public EventWrapper(DomainEvent @event, Guid aggregateId, string aggregateType, int eventNumber, Guid streamStateId)
        {
            Event = @event;
            AggregateType = aggregateType;
            AggregateId = aggregateId;
            EventNumber = eventNumber;
            StreamStateId = streamStateId;
        }
    }
}
