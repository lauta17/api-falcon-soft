using domain.entities;
using web.mappers.abstractions;
using web.models.users;

namespace web.mappers.authenticate
{
    public class AuthenticateResponseMapper : IAuthenticateResponseMapper
    {
        public AuthenticateResponse Map(User user, string jwt) 
        {
            return new AuthenticateResponse
            {
                Id = user.Id,
                Username = user.Name,
                JwtToken = jwt
            };
        }
    }
}
