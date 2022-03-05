using AutoMoqCore;
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
    public class GetUserByIdRepositoryTest
    {
        private readonly AutoMoqer _autoMoq;
        private readonly SqlDbContext _sqlDbContext;
        private readonly IGetUserByIdRepository _getUserByIdRepository;

        public GetUserByIdRepositoryTest()
        {
            _sqlDbContext = DataBase.Initialize();

            _autoMoq = new AutoMoqer();
            _getUserByIdRepository = new GetUserByIdRepository(_sqlDbContext, _autoMoq.Resolve<UserMapper>());
        }

        [Fact]
        public void whenUserFound_mustReturnIt() 
        {
            var userId = 3;
            var userDb = new UserDb { Id = 3, Name = "Lautaro", Password = "1234" };

            _sqlDbContext.Users.Add(userDb);
            _sqlDbContext.SaveChanges();

            var result = _getUserByIdRepository.Execute(userId).Result;

            Assert.Equal(userDb.Id, result.Id);
            Assert.Equal(userDb.Name, result.Name);
            Assert.Equal(userDb.Password, result.Password);
        }

        [Fact]
        public void whenUserNotFound_mustThrowException() 
        {
            Assert.ThrowsAsync<RepositoryException>(
                () => _getUserByIdRepository.Execute(3));
        }
    }
}
