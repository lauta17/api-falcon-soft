using domain.entities;

namespace domain.repositories.orders
{
    public interface IUpdateOrderRepository
    {
        Task Execute(Order order);
    }
}
