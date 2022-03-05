using domain.entities;
using web.mappers.abstractions;
using web.models.products;

namespace web.mappers.products
{
    public class ProductResponseMapper : IProductResponseMapper
    {
        public ProductResponse Map(Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Price = product.Price,
                Currency = product.Currency,
                Type = product.Type
            };
        }

        public List<ProductResponse> Map(List<Product> products)
        {
            List<ProductResponse> productsResponse = new List<ProductResponse>();

            foreach (var product in products)
            {
                productsResponse.Add(new ProductResponse
                {
                    Id = product.Id,
                    Price = product.Price,
                    Currency = product.Currency,
                    Type = product.Type
                });
            }

            return productsResponse;
        }
    }
}
