using domain.enums;

namespace web.models.products
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public CurrencyType Currency { get; set; }
        public ProductType Type { get; set; }
    }
}
