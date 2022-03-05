using domain.enums;

namespace domain.entities
{
    public class Product
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public CurrencyType Currency { get; set; }
        public ProductType Type { get; set; }

        public Product() 
        {
        }
    }
}
