using domain.entities;
using web.models.products;

namespace web.mappers.abstractions
{
    public interface IProductRequestMapper
    {
        Product Map(int id, ProductRequest productRequest);
    }
}
