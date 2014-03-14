using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.Application.Infrastructure
{
    public class EventStream
    {
        public IEnumerable<DomainEvent> Events { get; private set; }
        public StreamState State { get; private set; }

        public EventStream(IEnumerable<DomainEvent> events, StreamState state)
        {
            Events = events;
            State = state;
        }
    }
}
