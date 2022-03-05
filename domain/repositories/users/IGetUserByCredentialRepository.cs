using domain.entities;

namespace domain.repositories.users
{
    public interface IGetUserByCredentialRepository
    {
        Task<User> Execute(User user);
    }
}
