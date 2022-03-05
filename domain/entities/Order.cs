namespace domain.entities
{
    public class Order
    {
        #region Properties

        public int Id { get; set; }
        public decimal TotalPrice => CalculateTotalPrice();
        public List<Product> Products { get; private set; }

        #endregion

        #region Construtors
        public Order(List<Product> products) 
        {
            if(!products.Any())
            {
                throw new Exception("Order needs at least one product.");
            }
            Products = new List<Product>();
            AddProducts(products);
        }

        public Order(int id, List<Product> products)
        {
            if (!products.Any())
            {
                throw new Exception("Order needs at least one product.");
            }

            Id = id;
            Products = new List<Product>();
            AddProducts(products);
        }

        #endregion

        #region Public Methods

        public void AddProducts(List<Product> products)
        {
            Products.AddRange(products);
        }

        public void UpdateProduct(Product product) 
        {
            Products.Remove(Products.First(x => x.Id == product.Id));
            Products.Add(product);
        }

        #endregion

        #region Private Methods

        private decimal CalculateTotalPrice() 
        {
            return Products.Sum(product => product.Price);
        }

        #endregion
    }
}
