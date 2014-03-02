using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.Application.Model.Products
{
    public class Product
    {
        public Guid Id { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
