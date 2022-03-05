using domain.entities;

namespace application.usecases.products.abstractions
{
    public interface IGetProducts
    {
        Task<List<Product>> Execute();
    }
}
