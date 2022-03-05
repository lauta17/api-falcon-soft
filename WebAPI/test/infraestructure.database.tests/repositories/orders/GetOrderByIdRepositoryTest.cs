using AutoMoqCore;
using domain.exceptions;
using domain.repositories.orders;
using infraestructure.database.tests.helpers;
using infrastructure.database;
using infrastructure.database.mappers;
using infrastructure.database.model;
using infrastructure.database.repositories.orders;
using System.Collections.Generic;
using Xunit;

namespace infraestructure.database.tests.repositories.orders
{
    public class GetOrderByIdRepositoryTest
    {
        private readonly AutoMoqer _autoMoq;
        private readonly SqlDbContext _sqlDbContext;

        private readonly IGetOrderByIdRepository _getOrderByIdRepository;

        public GetOrderByIdRepositoryTest()
        {
            _sqlDbContext = DataBase.Initialize();

            _autoMoq = new AutoMoqer();
            _getOrderByIdRepository = new GetOrderByIdRepository(_sqlDbContext, new OrderMapper(_autoMoq.Resolve<ProductMapper>()));
        }

        [Fact]
        public void whenOrderFound_mustReturnIt()
        {
            var orderId = 15;
            var orderDb = new OrderDb 
            { 
                Id = orderId, 
                TotalPrice = 1000, 
                Products = new List<ProductDb> 
                { 
                    new ProductDb { Id = 1, CurrencyId = 1, ProductTypeId = 1, Price = 1000 } 
                }
            };

            _sqlDbContext.Orders.Add(orderDb);
            _sqlDbContext.SaveChanges();

            var result = _getOrderByIdRepository.Execute(orderId).Result;

            Assert.Equal(orderDb.Id, result.Id);
            Assert.Equal(orderDb.TotalPrice, result.TotalPrice);
        }

        [Fact]
        public void whenProductNotFound_mustThrowException()
        {
            Assert.ThrowsAsync<RepositoryException>(
                () => _getOrderByIdRepository.Execute(3));
        }
    }
}
