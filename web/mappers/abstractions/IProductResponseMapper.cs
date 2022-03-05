using domain.entities;
using web.models.products;

namespace web.mappers.abstractions
{
    public interface IProductResponseMapper
    {
        ProductResponse Map(Product product);
        List<ProductResponse> Map(List<Product> product);
    }
}
