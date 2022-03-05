using domain.entities;
using web.mappers.abstractions;
using web.models.orders;

namespace web.mappers.orders
{
    public class OrderResponseMapper : IOrderResponseMapper
    {
        private readonly IProductResponseMapper _productResponseMapper;
        public OrderResponseMapper(IProductResponseMapper productResponseMapper)
        {
            _productResponseMapper = productResponseMapper;
        }

        public List<OrderResponse> Map(List<Order> orders)
        {
            var ordersResponse = new List<OrderResponse>();

            foreach (var order in orders)
            {
                ordersResponse.Add(
                        new OrderResponse
                        {
                            Id = order.Id,
                            TotalPrice = order.TotalPrice,
                            Products = _productResponseMapper.Map(order.Products)
                        }
                    );
            }

            return ordersResponse;
        }
    }
}
