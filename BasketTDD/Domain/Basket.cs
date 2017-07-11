using BasketTDD.Domain.Offers;
using System.Collections.Generic;
using System.Linq;

namespace BasketTDD.Domain
{
    class Basket
    {
        public List<Product> Products { get; } = new List<Product>();
        private readonly HashSet<Offer> _offers = new HashSet<Offer>();

        internal void AddProducts(IEnumerable<Product> products) => Products.AddRange(products);

        internal decimal GetTotal()
        {
            var total = Products.Sum(x => x.Cost);
            total -= GetDiscountsFromOffers();

            return total;
        }

        private decimal GetDiscountsFromOffers()
        {
            var discounts = 0m;
            foreach (var offer in _offers)
                discounts += offer.GetDiscountCalculator()(Products);
            return discounts;
        }

        internal Basket WithOffers(params Offer[] offers)
        {
            foreach (var offer in offers)
                _offers.Add(offer);
            
            return this;
        }
    }
}