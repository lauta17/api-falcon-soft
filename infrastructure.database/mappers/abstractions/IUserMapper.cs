using domain.entities;
using infrastructure.database.model;

namespace infrastructure.database.mappers.abstractions
{
    public interface IUserMapper
    {
        User Map(UserDb userDb);
    }
}
