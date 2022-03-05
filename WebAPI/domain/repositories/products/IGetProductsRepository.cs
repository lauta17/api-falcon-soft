using domain.entities;

namespace domain.repositories.products
{
    public interface IGetProductsRepository
    {
        Task<List<Product>> Execute();
    }
}
