using domain.entities;
using domain.exceptions;
using domain.repositories.products;
using infrastructure.database.abstractions;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.database.repositories.products
{
    public class UpdateProductRepository : IUpdateProductRepository
    {
        private readonly ISqlDbContext _sqlDbContext;

        public UpdateProductRepository(ISqlDbContext sqlDbContext)
        {
            _sqlDbContext = sqlDbContext;
        }

        public async Task Execute(Product product)
        {
            var productDb = await _sqlDbContext.Products.FirstOrDefaultAsync(x => x.Id == product.Id);

            if (productDb == null)
            {
                throw new RepositoryException("Product not found with id: {id}");
            }

            productDb.Price = product.Price;

            await _sqlDbContext.SaveChangesAsync();
        }
    }
}
