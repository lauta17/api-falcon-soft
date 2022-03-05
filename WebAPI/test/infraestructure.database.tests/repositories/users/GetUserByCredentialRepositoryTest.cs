using AutoMoqCore;
using domain.entities;
using domain.exceptions;
using domain.repositories.users;
using infraestructure.database.tests.helpers;
using infrastructure.database;
using infrastructure.database.mappers;
using infrastructure.database.model;
using infrastructure.database.repositories.users;
using System;
using Xunit;

namespace infraestructure.database.tests.repositories.users
{
    public class GetUserByCredentialRepositoryTest
    {
        private readonly AutoMoqer _autoMoq;
        private readonly SqlDbContext _sqlDbContext;
        private readonly IGetUserByCredentialRepository _getUserByCredentialRepository;

        public GetUserByCredentialRepositoryTest()
        {
            _sqlDbContext = DataBase.Initialize();

            _autoMoq = new AutoMoqer();
            _getUserByCredentialRepository = new GetUserByCredentialRepository(_sqlDbContext, _autoMoq.Resolve<UserMapper>());
        }

        [Fact]
        public void whenUserFound_mustReturnIt()
        {
            var userDb = new UserDb { Id = 3, Name = "Lautaro", Password = "1234" };

            _sqlDbContext.Users.Add(userDb);
            _sqlDbContext.SaveChanges();

            var result = _getUserByCredentialRepository.Execute(
                new User { Name = userDb.Name, Password = userDb.Password }
            ).Result;

            Assert.Equal(userDb.Id, result.Id);
            Assert.Equal(userDb.Name, result.Name);
            Assert.Equal(userDb.Password, result.Password);
        }

        [Fact]
        public void whenUserNotFound_mustThrowException()
        {
            Assert.ThrowsAsync<RepositoryException>(
                () => _getUserByCredentialRepository.Execute(new User { Name = "Lautaro", Password = "1234" }));
        }
    }
}
