using domain.entities;
using domain.exceptions;
using domain.repositories.users;
using infrastructure.database.abstractions;
using infrastructure.database.mappers.abstractions;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.database.repositories.users
{
    public class GetUserByIdRepository : IGetUserByIdRepository
    {
        private readonly ISqlDbContext _sqlDbContext;
        private readonly IUserMapper _userMapper;

        public GetUserByIdRepository(ISqlDbContext sqlDbContext,
            IUserMapper userMapper)
        {
            _sqlDbContext = sqlDbContext;
            _userMapper = userMapper;
        }

        public async Task<User> Execute(int id)
        {
            var userDb = await _sqlDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userDb == null) 
            {
                throw new RepositoryException($"User not found with id: {id}.");
            }

            return _userMapper.Map(userDb);
        }
    }
}
