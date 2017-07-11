using System;
using System.Collections.Generic;
using System.Linq;

namespace BasketTDD.Domain.Offers
{
    class ButterOffer : Offer
    {
        internal override Func<IEnumerable<Product>, decimal> GetDiscountCalculator()
        {
            return products =>
            {
                var numberOfButters = products.Count(product => product.Name == "Butter");
                var numberOfDiscounts = numberOfButters / 2;
                var numberOfBreads = products.Count(product => product.Name == "Bread");
                var breadPrice = products.FirstOrDefault(product => product.Name == "Bread")?.Cost;

                return Math.Min(numberOfDiscounts, numberOfBreads) * breadPrice / 2 ?? 0;
            };
        }
    }
}