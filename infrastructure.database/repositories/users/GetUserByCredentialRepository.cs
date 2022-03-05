using domain.entities;
using domain.exceptions;
using domain.repositories.users;
using infrastructure.database.abstractions;
using infrastructure.database.mappers.abstractions;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.database.repositories.users
{
    public class GetUserByCredentialRepository : IGetUserByCredentialRepository
    {
        private readonly ISqlDbContext _sqlDbContext;
        private readonly IUserMapper _userMapper;

        public GetUserByCredentialRepository(ISqlDbContext sqlDbContext,
            IUserMapper userMapper)
        {
            _sqlDbContext = sqlDbContext;
            _userMapper = userMapper;
        }

        public async Task<User> Execute(User user)
        {
            var userDb = await _sqlDbContext.Users
                            .FirstOrDefaultAsync(x => 
                                (x.Name.ToUpper().Equals(user.Name.ToUpper())
                                && x.Password == user.Password));

            if (userDb == null) 
            {
                throw new RepositoryException($"User not found with credentials userName: {user.Name}, {user.Password}.");
            }

            return _userMapper.Map(userDb);
        }
    }
}
