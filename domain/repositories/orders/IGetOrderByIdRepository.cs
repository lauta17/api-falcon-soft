using domain.entities;

namespace domain.repositories.orders
{
    public interface IGetOrderByIdRepository
    {
        Task<Order> Execute(int id);
    }
}
