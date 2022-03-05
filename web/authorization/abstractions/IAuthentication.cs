using domain.entities;

namespace web.authorization.abstractions
{
    public interface IAuthentication
    {
        Task<User> Login(User user);
    }
}
