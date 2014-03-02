using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPPDDDChap23.EventSourcing.Application.Infrastructure;

namespace PPPDDDChap23.EventSourcing.Application.Model.Orders
{
    public class OrderCreated : IDomainEvent
    {
        public Guid Id {get; set;}
        public Guid CustomerId {get; set;}
        public IEnumerable<OrderItem> ItemsOnOrder { get; set; }
    }
}
