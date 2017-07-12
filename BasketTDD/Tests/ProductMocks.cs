using BasketTDD.Domain;

namespace BasketTDD.Tests
{
    static class ProductMocks
    {
        internal static Product Bread { get { return new Product(ApplicationConstants.Bread, 1); } }
        internal static Product Butter { get { return new Product(ApplicationConstants.Butter, 0.8m); } }
        internal static Product Milk { get { return new Product(ApplicationConstants.Milk, 1.15m); } }
    }
}
