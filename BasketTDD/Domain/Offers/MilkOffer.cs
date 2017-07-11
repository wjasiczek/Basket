using System;
using System.Collections.Generic;
using System.Linq;

namespace BasketTDD.Domain.Offers
{
    class MilkOffer : Offer
    {
        internal override Func<IEnumerable<Product>, decimal> GetDiscountCalculator()
        {
            return products =>
            {
                var numberOfMilks = products.Count(x => x.Name == "Milk");
                var numberOfDiscounts = numberOfMilks / 4;
                var milkPrice = products.FirstOrDefault(x => x.Name == "Milk")?.Cost;

                return numberOfDiscounts * milkPrice ?? 0;
            };
        }
    }
}