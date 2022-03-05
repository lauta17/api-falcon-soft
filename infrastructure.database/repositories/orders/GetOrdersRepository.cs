using domain.dtos;
using domain.entities;
using domain.repositories.orders;
using infrastructure.database.abstractions;
using infrastructure.database.mappers.abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace infrastructure.database.repositories.orders
{
    public class GetOrdersRepository : IGetOrdersRepository
    {
        private readonly ISqlDbContext _dbContext;
        private readonly IOrderMapper _orderMapper;

        public GetOrdersRepository(ISqlDbContext dbContext,
            IOrderMapper orderMapper)
        {
            _dbContext = dbContext;
            _orderMapper = orderMapper;
        }

        public async Task<List<Order>> Execute(PaginationDto paginationDto, OrderFiltersDto ordersFilterDto)
        {
            var ordersDb = _dbContext.Orders
                .OrderBy(paginationDto.OrderBy)
                .Include(order => order.Products)
                .Skip(paginationDto.Offset)
                .Take(paginationDto.Limit)
                .Where(order => 
                       (ordersFilterDto.Id.HasValue ? order.Id == ordersFilterDto.Id : true) 
                           && (ordersFilterDto.TotalPrice > 0 ? order.TotalPrice == ordersFilterDto.TotalPrice : true))
                .ToListAsync();

            var orders = new List<Order>();

            foreach (var orderDb in await ordersDb)
            {
                orders.Add(_orderMapper.Map(orderDb));
            }

            return orders;
        }
    }
}
