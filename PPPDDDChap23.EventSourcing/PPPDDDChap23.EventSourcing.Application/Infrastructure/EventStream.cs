using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.Application.Infrastructure
{
    public class EventStream
    {
        public IEnumerable<DomainEvent> Events { get; set; }
        public int Version {get; set;}
        public Guid AggregateId { get; set; }

        public EventStream(IEnumerable<DomainEvent> events, int version, Guid aggregateId)
        {
            Events = events;
            Version = version;
            AggregateId = aggregateId;
        }
    }
}
