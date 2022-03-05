using domain.entities;
using web.models.orders;

namespace web.mappers.abstractions
{
    public interface IOrderResponseMapper
    {
        List<OrderResponse> Map(List<Order> orders);
    }
}
