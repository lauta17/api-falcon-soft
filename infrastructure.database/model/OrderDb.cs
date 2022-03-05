namespace infrastructure.database.model
{
    public class OrderDb
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public List<ProductDb> Products { get; set; }

        public OrderDb() 
        {
            Products = new List<ProductDb>();
        }
    }
}
