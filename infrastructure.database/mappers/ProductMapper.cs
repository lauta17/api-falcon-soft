using domain.entities;
using domain.enums;
using infrastructure.database.mappers.abstractions;
using infrastructure.database.model;

namespace infrastructure.database.mappers
{
    public class ProductMapper : IProductMapper
    {
        public Product Map(ProductDb productDb)
        {
            return new Product
            {
                Id = productDb.Id,
                Price = productDb.Price,
                Currency = (CurrencyType)productDb.CurrencyId,
                Type = (ProductType)productDb.ProductTypeId
            };
        }
    }
}
