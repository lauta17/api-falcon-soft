using domain.dtos;
using domain.entities;

namespace domain.repositories.orders
{
    public interface IGetOrdersRepository
    {
        Task<List<Order>> Execute(PaginationDto paginationDto, OrderFiltersDto ordersFilterDto);
    }
}
