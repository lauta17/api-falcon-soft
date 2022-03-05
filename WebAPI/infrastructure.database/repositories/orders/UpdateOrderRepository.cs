using domain.entities;
using domain.exceptions;
using domain.repositories.orders;
using infrastructure.database.abstractions;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.database.repositories.orders
{
    public class UpdateOrderRepository : IUpdateOrderRepository
    {
        private readonly ISqlDbContext _sqlDbContext;

        public UpdateOrderRepository(ISqlDbContext sqlDbContext)
        {
            _sqlDbContext = sqlDbContext;
        }

        public async Task Execute(Order order)
        {
            var orderDb = await _sqlDbContext.Orders.FirstOrDefaultAsync(order => order.Id == order.Id);

            if (orderDb == null) 
            {
                throw new RepositoryException($"Order not found for update with id {order.Id}");
            }

            orderDb.TotalPrice = order.TotalPrice;

            await _sqlDbContext.SaveChangesAsync();
        }
    }
}
