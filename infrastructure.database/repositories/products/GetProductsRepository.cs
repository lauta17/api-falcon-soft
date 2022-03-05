using domain.entities;
using domain.repositories.products;
using infrastructure.database.abstractions;
using infrastructure.database.mappers.abstractions;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.database.repositories.products
{
    public class GetProductsRepository : IGetProductsRepository
    {
        private readonly ISqlDbContext _dbContext;
        private readonly IProductMapper _productMapper;

        public GetProductsRepository(ISqlDbContext dbContext,
            IProductMapper productMapper)
        {
            _dbContext = dbContext;
            _productMapper = productMapper;
        }

        public async Task<List<Product>> Execute()
        {
            var productsDb = _dbContext.Products.ToListAsync();

            var products = new List<Product>();

            foreach (var productDb in await productsDb)
            {
                products.Add(_productMapper.Map(productDb));
            }

            return products;
        }
    }
}
