using domain.entities;
using infrastructure.database.mappers.abstractions;
using infrastructure.database.model;

namespace infrastructure.database.mappers
{
    public class OrderMapper : IOrderMapper
    {
        private readonly IProductMapper _productMapper;

        public OrderMapper(IProductMapper productMapper)
        {
            _productMapper = productMapper;
        }

        public Order Map(OrderDb orderDb)
        {
            List<Product> products = new List<Product>();

            foreach (var productsDb in orderDb.Products)
            {
                products.Add(_productMapper.Map(productsDb));
            }

            return new Order(orderDb.Id, products);
        }
    }
}
