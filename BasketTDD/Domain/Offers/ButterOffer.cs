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
                var numberOfButters = products.Count(product => product.Name == ApplicationConstants.Butter);
                var numberOfDiscounts = numberOfButters / 2;
                var numberOfBreads = products.Count(product => product.Name == ApplicationConstants.Bread);
                var breadPrice = products.FirstOrDefault(product => product.Name == ApplicationConstants.Bread)?.Cost;

                return Math.Min(numberOfDiscounts, numberOfBreads) * breadPrice / 2 ?? 0;
            };
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            var butterOffer = obj as ButterOffer;

            return butterOffer != null;
        }

        public bool Equals(ButterOffer butterOffer) => butterOffer != null;

        public override int GetHashCode() => GetType().AssemblyQualifiedName.GetHashCode();
    }
}