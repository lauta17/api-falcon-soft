namespace infrastructure.database.model
{
    public class ProductDb
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int CurrencyId { get; set; }
        public int ProductTypeId { get; set; }
        public OrderDb Order { get; set; }

        public ProductDb() 
        {
            Order = new OrderDb();
        }
    }
}
