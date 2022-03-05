using domain.entities;
using web.mappers.abstractions;
using web.models.users;

namespace web.mappers.authenticate
{
    public class AuthenticateRequestMapper : IAuthenticateRequestMapper
    {
        public User Map(AuthenticateRequest authenticateRequest) 
        {
            return new User
            {
                Name = authenticateRequest.UserName,
                Password = authenticateRequest.Password
            };
        }
    }
}
