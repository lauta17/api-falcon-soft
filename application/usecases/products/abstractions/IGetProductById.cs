using domain.entities;

namespace application.usecases.products.abstractions
{
    public interface IGetProductById
    {
        Task<Product> Execute(int id);
    }
}
