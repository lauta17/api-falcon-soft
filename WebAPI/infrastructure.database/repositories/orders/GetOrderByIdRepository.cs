using domain.entities;
using domain.exceptions;
using domain.repositories.orders;
using infrastructure.database.abstractions;
using infrastructure.database.mappers.abstractions;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.database.repositories.orders
{
    public class GetOrderByIdRepository : IGetOrderByIdRepository
    {
        private readonly ISqlDbContext _sqlDbContext;
        private readonly IOrderMapper _orderMapper;

        public GetOrderByIdRepository(ISqlDbContext sqlDbContext,
            IOrderMapper orderMapper)
        {
            _sqlDbContext = sqlDbContext;
            _orderMapper = orderMapper;
        }

        public async Task<Order> Execute(int id)
        {
            var orderDb = await _sqlDbContext.Orders
                .Include(order => order.Products)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (orderDb == null) 
            {
                throw new RepositoryException($"Order not found with id: {id}");
            }

            return _orderMapper.Map(orderDb);
        }
    }
}
