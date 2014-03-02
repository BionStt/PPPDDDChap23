using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.Application.Model.Orders
{
    public interface IOpenOrderRepository
    {
        void Add(OpenOrder openOrder);
        OpenOrder FindBy(Guid id);
    }
}
