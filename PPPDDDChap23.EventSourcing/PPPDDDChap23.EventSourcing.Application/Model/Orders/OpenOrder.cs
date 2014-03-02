using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPPDDDChap23.EventSourcing.Application.Infrastructure;

namespace PPPDDDChap23.EventSourcing.Application.Model.Orders
{
    public class OpenOrder : EventSourcedAggregate<Guid>
    {
        public Guid CutomerId { get; set; }
        public IList<OrderItem> ItemsOnOrder { get; set; }

        public OpenOrder(Guid id, Guid customerId, IEnumerable<Item> items, DispatchAddress address)
        {
            Causes(new OrderCreated
            {
                Id = id,
                CustomerId = customerId,
                ItemsOnOrder = ConvertToOrderItems(items)                
            });
        }

        public void PriceMatchItem()
        { 
        
        }

        private void Causes(IDomainEvent @event)
        {
            When((dynamic)@event);
            Changes.Add(@event);
        }

        private void When(OrderCreated orderCreated)
        {
            this.Id = orderCreated.Id;
            this.CutomerId = orderCreated.CustomerId;
            this.ItemsOnOrder = orderCreated.ItemsOnOrder.ToList();
        }

        private IList<OrderItem> ConvertToOrderItems(IEnumerable<Item> items)
        {
            return items.Select(item => new OrderItem(item.ProductId, item.Quantity, item.UnitPrice)).ToList();                
        }

        public void AdjustItemQuantity(Item item)
        {
            if (OrderContainsAnOrderItemFor(item.ProductId))
            {
                if (item.Quantity.is_zero())
                    RemoveOrderItemForProductWithIdOf(item.ProductId);
                else
                    GetOrderItemFor(item.ProductId).ChangeQuantityTo(item.Quantity);
            }
        }

        public void Add(Item item)
        {            
            if (basket_contains_an_item_for(product))
                get_item_for(product).increase_item_quantity_by(new NonNegativeQuantity(1));
            else
                _items.Add(BasketItemFactory.create_item_for(product));
        }

        private OrderItem GetOrderItemFor(Guid productId)
        {
            return ItemsOnOrder.First(i => i.ContainsProductsWithIdOf(productId));
        }

        private bool OrderContainsAnOrderItemFor(Guid productId)
        {
            return ItemsOnOrder.Any(i => i.ContainsProductsWithIdOf(productId));
        }

        public void RemoveAnItem()
        { 
                        
        }

        public void AddDiscount() // Apply to entire order, reason for the adjustment.
        { }

        public void ChangeShippingAddress() // Throw Exception if trying to change country
        { }

        public void Cancel()
        { 
        
        }

        public void SubmitOrder()
        { 
           
        }
    }
}
