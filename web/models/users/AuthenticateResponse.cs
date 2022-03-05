namespace web.models.users
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }

        public AuthenticateResponse() 
        {
            Username = string.Empty;
            JwtToken = string.Empty;
        }
    }
}