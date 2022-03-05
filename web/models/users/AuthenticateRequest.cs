namespace web.models.users
{
    public class AuthenticateRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public AuthenticateRequest() 
        {
            UserName = string.Empty;
            Password = string.Empty;
        }
    }
}
