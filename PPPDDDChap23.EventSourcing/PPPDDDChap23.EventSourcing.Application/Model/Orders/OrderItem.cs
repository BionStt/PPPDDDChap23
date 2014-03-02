using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.Application.Model.Orders
{
    public class OrderItem
    {
        public OrderItem(Guid productid, int quantity, decimal unit_price)
        {
            this.ProductId = productid;
            this.Quantity = quantity;
            this.UnitPrice = unit_price;
        }

        public Guid ProductId { get; private set; }

        public int Quantity { get; private set; }

        public decimal UnitPrice { get; private set; }
    }
}
