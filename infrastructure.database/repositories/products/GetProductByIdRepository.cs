using domain.entities;
using domain.exceptions;
using domain.repositories.products;
using infrastructure.database.abstractions;
using infrastructure.database.mappers.abstractions;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.database.repositories.products
{
    public class GetProductByIdRepository : IGetProductByIdRepository
    {
        private readonly ISqlDbContext _dbContext;
        private readonly IProductMapper _productMapper;
        public GetProductByIdRepository(ISqlDbContext dbContext,
            IProductMapper productMapper) 
        {
            _dbContext = dbContext;
            _productMapper = productMapper;
        }

        public async Task<Product> Execute(int id)
        {
            var product = await _dbContext.Products.Where(product => product.Id == id).FirstOrDefaultAsync();

            if (product == null) 
            {
                throw new RepositoryException($"Product not found with id: {id}");
            }

            return _productMapper.Map(product);
        }
    }
}
