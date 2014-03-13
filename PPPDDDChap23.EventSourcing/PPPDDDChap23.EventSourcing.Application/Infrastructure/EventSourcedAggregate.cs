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
        protected int Version { get; set; }

        public EventSourcedAggregate()
        {
            Version = 0;
            Changes = new List<DomainEvent>();
        }

        public EventStream GetChanges()
        {
            var eventStream = new EventStream(Changes, Version, this.Id);

            return eventStream; 
        }
    }
}
