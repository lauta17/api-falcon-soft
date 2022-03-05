using domain.entities;
using web.models.users;

namespace web.mappers.abstractions
{
    public interface IAuthenticateRequestMapper
    {
        User Map(AuthenticateRequest authenticateRequest);
    }
}
