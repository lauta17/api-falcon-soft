using domain.entities;
using domain.repositories.users;
using domain.utils;
using web.authorization.abstractions;

namespace web.authorization
{
    public class Authentication : IAuthentication
    {
        private readonly IGetUserByCredentialRepository _getUserByCredentialRepository;

        public Authentication(IGetUserByCredentialRepository getUserByCredentialRepository)
        {
            _getUserByCredentialRepository = getUserByCredentialRepository;
        }

        public async Task<User> Login(User user)
        {
            user.Password = Encrypt.EncryptString(user.Password, "QWER");

            user = await _getUserByCredentialRepository.Execute(user);

            if (user != null)
            {
                user.Password = string.Empty;
            }

            return user;
        }
    }
}
