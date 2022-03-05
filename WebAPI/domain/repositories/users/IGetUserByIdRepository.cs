using domain.entities;

namespace domain.repositories.users
{
    public interface IGetUserByIdRepository
    {
        Task<User> Execute(int id);
    }
}
