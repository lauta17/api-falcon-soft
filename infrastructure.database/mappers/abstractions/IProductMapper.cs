using domain.entities;
using infrastructure.database.model;

namespace infrastructure.database.mappers.abstractions
{
    public interface IProductMapper
    {
        Product Map(ProductDb productDb);
    }
}
