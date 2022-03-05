using domain.entities;
using web.mappers.abstractions;
using web.models.products;

namespace web.mappers.products
{
    public class ProductRequestMapper : IProductRequestMapper
    {
        public Product Map(int id, ProductRequest productRequest)
        {
            return new Product
            {
                Id = id,
                Price = productRequest.Price
            };
        }
    }
}
