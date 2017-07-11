using System;
using System.Collections.Generic;

namespace BasketTDD.Domain.Offers
{
    abstract class Offer
    {
        internal abstract Func<IEnumerable<Product>, decimal> GetDiscountCalculator();
    }
}