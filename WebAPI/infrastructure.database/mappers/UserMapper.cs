using domain.entities;
using infrastructure.database.mappers.abstractions;
using infrastructure.database.model;

namespace infrastructure.database.mappers
{
    public class UserMapper : IUserMapper
    {
        public User Map(UserDb userDb) 
        {
            return new User
            {
                Id = userDb.Id,
                Password = userDb.Password,
                Name = userDb.Name
            };
        }
    }
}
