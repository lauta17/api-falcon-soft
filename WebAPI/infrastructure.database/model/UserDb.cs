namespace infrastructure.database.model
{
    public class UserDb
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public UserDb() 
        {
            Name = string.Empty;
            Password = string.Empty;
        }
    }
}
