using System;
using System.Collections.Generic;
using System.Linq;
using Agathas.Storefront.Common;
using Agathas.Storefront.Infrastructure;
using Agathas.Storefront.Shopping.Baskets.Events;
using Agathas.Storefront.Shopping.Model.Promotions;

namespace Agathas.Storefront.Shopping.Model.Baskets
{
    public class Basket 
    {
        private IList<BasketItem> _items;
        private IList<Coupon> _coupons;
        private IList<IEvent> _changes;
        private Guid _id;        
               
        private Basket()
        {
        }

        public Basket(Guid id)
        {
            _id = id;
            _items = new List<BasketItem>();            

            DomainEvents.raise(new BasketCreated(this._id, new Money()));
        }

        public Guid id
        {
            get { return _id; }
        }

        private void Apply(IEvent @event)
        {
            When((dynamic)@event);
            _changes.Add(@event);
        }

        private void When(AmountWithdrawn @event)
        {
            _amount = _amount.Substract(@event.Amount);
        }

        private void When(AmountDeposited @event)
        {
            _amount = _amount.Add(@event.Amount);
        }
        
        public void add(Product product)
        {
            // TODO: Check for null values and invalid data

            if (basket_contains_an_item_for(product))
                get_item_for(product).increase_item_quantity_by(new NonNegativeQuantity(1));
            else
                _items.Add(BasketItemFactory.create_item_for(product_snapshot));  
        }

        private BasketItem get_item_for(Product product)
        {
            return _items.Where(i => i.contains(product)).FirstOrDefault();
        }

        private bool basket_contains_an_item_for(Product product)
        {
            return _items.Any(i => i.contains(product));
        }

        public void remove_product_with_id_of(Guid productId)
        {
            if (basket_contains_an_item_for(product))
            {
                _items.Remove(get_item_for(product));

                recalculate_basket_totals(basket_pricing_service);
            }
        }

        private void recalculate_basket_totals(IBasketPricingService basket_pricing_service)
        {
            var total = basket_pricing_service.calculate_total_price_for(_items, _coupons);

            DomainEvents.raise(new BasketPriceChanged(this._id, total.basket_total, total.discount));
        }

        public void change_quantity_of_product(NonNegativeQuantity quantity, ProductSnapshot product_snapshot, IBasketPricingService basket_pricing_service)
        {
            // TODO: Check for null values and invalid data

            if (basket_contains_an_item_for(product_snapshot))
            {
                if (quantity.is_zero())
                {
                    remove_product_with_id_of(product_snapshot, basket_pricing_service);
                }
                else
                    get_item_for(product_snapshot).change_item_quantity_to(quantity);

                recalculate_basket_totals(basket_pricing_service);
            }
        }
                      
        public bool contains_product(Func<BasketItem, bool> func)
        {
            return _items.Any(func);
        }
     
        public void apply(Promotion promotion, IBasketPricingService basket_pricing_service)
        {
            // double dispatch
            var coupon = promotion.create_coupon_for(this._id); 

            _coupons.Add(coupon);

            recalculate_basket_totals(basket_pricing_service);
        }

        public void remove_coupon(string coupon_code, IBasketPricingService basket_pricing_service)
        {
            var coupon = _coupons.First(c => c.code == coupon_code);

            _coupons.Remove(coupon);

            recalculate_basket_totals(basket_pricing_service);
        }  
    }
}

