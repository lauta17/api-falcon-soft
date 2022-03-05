using application.usecases.orders.abstractions;
using domain.dtos;
using domain.entities;
using domain.repositories.orders;

namespace application.usecases.orders
{
    public class GetFilteredOrders : IGetFilteredOrders
    {
        private readonly IGetOrdersRepository _getOrdersRepository;

        public GetFilteredOrders(IGetOrdersRepository getOrdersRepository)
        {
            _getOrdersRepository = getOrdersRepository;
        }
        public async Task<List<Order>> Execute(PaginationDto paginationDto, OrderFiltersDto orderFilterDto)
        {
            return await _getOrdersRepository.Execute(paginationDto, orderFilterDto);
        }
    }
}
