using domain.entities;
using web.models.users;

namespace web.mappers.abstractions
{
    public interface IAuthenticateResponseMapper
    {
        AuthenticateResponse Map(User user, string jwt);
    }
}
