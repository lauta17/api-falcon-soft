namespace domain.entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Password { set; get; }

        public User() 
        {
            Name = string.Empty;
            Password = string.Empty;
        }
    }
}
