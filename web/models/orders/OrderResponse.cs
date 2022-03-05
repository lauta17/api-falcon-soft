using web.models.products;

namespace web.models.orders
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public List<ProductResponse> Products { get; set; }

        public OrderResponse() 
        {
            Products = new List<ProductResponse>();
        }
    }
}
