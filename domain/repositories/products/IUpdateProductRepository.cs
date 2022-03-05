using domain.entities;

namespace domain.repositories.products
{
    public interface IUpdateProductRepository
    {
        Task Execute(Product product);
    }
}
