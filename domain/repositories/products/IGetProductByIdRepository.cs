using domain.entities;

namespace domain.repositories.products
{
    public interface IGetProductByIdRepository
    {
        Task<Product> Execute(int id);
    }
}
