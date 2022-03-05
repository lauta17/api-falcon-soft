using domain.dtos;
using domain.entities;

namespace application.usecases.orders.abstractions
{
    public interface IGetFilteredOrders
    {
        //Task<List<Order>> Execute(string orderBy, int limit = 50, int offset = 0);
        Task<List<Order>> Execute(PaginationDto paginationDto, OrderFiltersDto ordersFilterDto);
    }
}
