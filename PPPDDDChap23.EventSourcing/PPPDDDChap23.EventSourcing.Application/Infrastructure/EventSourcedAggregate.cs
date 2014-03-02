using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.Application.Infrastructure
{
    public abstract class EventSourcedAggregate<TId> : Entity<TId>
    {
        protected List<IDomainEvent> Changes { get; private set; }

        public EventSourcedAggregate()
        {
            Changes = new List<IDomainEvent>();
        }    
    }
}
