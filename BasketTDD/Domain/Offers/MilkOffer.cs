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
                var numberOfMilks = products.Count(x => x.Name == ApplicationConstants.Milk);
                var numberOfDiscounts = numberOfMilks / 4;
                var milkPrice = products.FirstOrDefault(x => x.Name == ApplicationConstants.Milk)?.Cost;

                return numberOfDiscounts * milkPrice ?? 0;
            };
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            var milkOffer = obj as MilkOffer;

            return milkOffer != null;
        }

        public bool Equals(MilkOffer milkOffer) => milkOffer != null;

        public override int GetHashCode() => GetType().AssemblyQualifiedName.GetHashCode();
    }
}