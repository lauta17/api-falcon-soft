using domain.entities;

namespace application.usecases.products.abstractions
{
    public interface IModifyProduct
    {
        Task Execute(int orderId, Product product);
    }
}
