using BasketTDD.Domain;
using BasketTDD.Domain.Offers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BasketTDD.Tests
{
    class BasketTests
    {
        [Test]
        public void Name_And_Cost_Should_Create_Correct_Product()
        {
            var product = ProductMocks.Bread;

            Assert.AreEqual("Bread", product.Name);
            Assert.AreEqual(1m, product.Cost);
        }

        [Test]
        public void Add_Products_Basket_Should_Have_Those_Products()
        {
            var basket = new Basket();
            var products = new List<Product>
            {
                new Product("0", 0),
                new Product("1", 1)
            };

            basket.AddProducts(products);

            Assert.AreEqual(products.Count, basket.Products.Count);
            Assert.AreSame(products[0], basket.Products[0]);
            Assert.AreSame(products[1], basket.Products[1]);
        }

        [Test]
        public void Basket_With_3Pounds_Products_Total_3Pounds()
        {
            var basket = new Basket();
            var products = new List<Product>
            {
                new Product(string.Empty, 1),
                new Product(string.Empty, 2)
            };
            basket.AddProducts(products);

            decimal total = basket.GetTotal();

            Assert.AreEqual(3m, total);
        }

        [Test]
        public void Offer_50Percent_Bread_Discount_Per_2Butter()
        {
            var butterOffer = new ButterOffer();
            var products = new List<Product>
            {
                ProductMocks.Bread,
                ProductMocks.Butter,
                ProductMocks.Butter
            };

            Func<IEnumerable<Product>, decimal> discountCalculator = butterOffer.GetDiscountCalculator();
            var discount = discountCalculator(products);

            Assert.AreEqual(0.5m, discount);
        }

        [Test]
        public void Offer_50Percent_Bread_Discount_Only_Once_With_2Bread_2Butter()
        {
            var butterOffer = new ButterOffer();
            var products = new List<Product>
            {
                ProductMocks.Bread,
                ProductMocks.Bread,
                ProductMocks.Butter,
                ProductMocks.Butter
            };

            Func<IEnumerable<Product>, decimal> discountCalculator = butterOffer.GetDiscountCalculator();
            var discount = discountCalculator(products);

            Assert.AreEqual(0.5m, discount);
        }

        [Test]
        public void Offer_50Percent_Bread_Discount_Only_Once_With_4Butter()
        {
            var butterOffer = new ButterOffer();
            var products = new List<Product>
            {
                ProductMocks.Bread,
                ProductMocks.Butter,
                ProductMocks.Butter,
                ProductMocks.Butter,
                ProductMocks.Butter
            };

            Func<IEnumerable<Product>, decimal> discountCalculator = butterOffer.GetDiscountCalculator();
            var discount = discountCalculator(products);

            Assert.AreEqual(0.5m, discount);
        }

        [Test]
        public void Offer_Every_Fourth_Milk_Free()
        {
            var milkOffer = new MilkOffer();
            var products = Enumerable.Repeat(ProductMocks.Milk, 4);

            Func<IEnumerable<Product>, decimal> discountCalculator = milkOffer.GetDiscountCalculator();
            var discount = discountCalculator(products);

            Assert.AreEqual(1.15m, discount);
        }

        [Test]
        public void Basket_1Bread_1Butter_1Milk_Total_2Pounds95()
        {
            var basket = new Basket();
            var products = new List<Product>
            {
                ProductMocks.Bread,
                ProductMocks.Butter,
                ProductMocks.Milk
            };
            basket.AddProducts(products);

            decimal total = basket.GetTotal();

            Assert.AreEqual(2.95m, total);
        }

        [Test]
        public void Basket_2Butter_2Bread_Total_3Pounds10()
        {
            var basket = new Basket();
            var products = new List<Product>
            {
                ProductMocks.Bread,
                ProductMocks.Bread,
                ProductMocks.Butter,
                ProductMocks.Butter
            };
            basket.AddProducts(products);
            var butterOffer = new ButterOffer();

            decimal total = basket
                .WithOffers(butterOffer)
                .GetTotal();

            Assert.AreEqual(3.1m, total);
        }

        [Test]
        public void Basket_4Milk_Total_3Pounds45()
        {
            var basket = new Basket();
            var products = Enumerable.Repeat(ProductMocks.Milk, 4);
            basket.AddProducts(products);
            var milkOffer = new MilkOffer();

            decimal total = basket
                .WithOffers(milkOffer)
                .GetTotal();

            Assert.AreEqual(3.45m, total);
        }

        [Test]
        public void Basket_2Butter_1Bread_8Milk_Total_9Pounds()
        {
            var basket = new Basket();
            var products = Enumerable.Repeat(ProductMocks.Milk, 8).ToList();
            products.AddRange(new List<Product>
            {
                ProductMocks.Bread,
                ProductMocks.Butter,
                ProductMocks.Butter
            });
            Enumerable.Repeat(ProductMocks.Milk, 8);
            basket.AddProducts(products);
            var butterOffer = new ButterOffer();
            var milkOffer = new MilkOffer();

            decimal total = basket
                .WithOffers(butterOffer, milkOffer)
                .GetTotal();

            Assert.AreEqual(9, total);
        }

        [Test]
        public void Basket_Duplicate_Offers_DoNot_Cumulate()
        {
            var basket = new Basket();
            var products = Enumerable.Repeat(ProductMocks.Milk, 8).ToList();
            products.AddRange(new List<Product>
            {
                ProductMocks.Bread,
                ProductMocks.Butter,
                ProductMocks.Butter
            });
            Enumerable.Repeat(ProductMocks.Milk, 8);
            basket.AddProducts(products);
            var butterOffer = new ButterOffer();
            var milkOffer = new MilkOffer();

            decimal total = basket
                .WithOffers(butterOffer, milkOffer, milkOffer, butterOffer)
                .GetTotal();

            Assert.AreEqual(9, total);
        }
    }
}
