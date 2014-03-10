using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.Application.Infrastructure
{
    public class EventRecord
    {
        public DomainEvent Event { get; set; }

        public EventRecord(DomainEvent evt)
        {
            Event = evt;
        }
    }
}
