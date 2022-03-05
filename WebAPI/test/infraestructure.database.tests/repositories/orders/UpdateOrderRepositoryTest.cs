using AutoMoqCore;
using domain.dtos;
using domain.entities;
using domain.enums;
using domain.exceptions;
using domain.repositories.orders;
using infraestructure.database.tests.helpers;
using infrastructure.database;
using infrastructure.database.model;
using infrastructure.database.repositories.orders;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace infraestructure.database.tests.repositories.orders
{
    public class UpdateOrderRepositoryTest
    {
        private readonly IUpdateOrderRepository _updateOrderRepository;

        private readonly AutoMoqer _autoMoqer;
        private readonly SqlDbContext _sqlDbContext;

        public UpdateOrderRepositoryTest()
        {
            _sqlDbContext = DataBase.Initialize();

            _autoMoqer = new AutoMoqer();
            _updateOrderRepository = new UpdateOrderRepository(_sqlDbContext);
        }

        [Fact]
        public void whenOrderFound_mustReturnUpdateIt()
        {
            var orderDb = new OrderDb
            {
                Id = 1,
                TotalPrice = 1000,
                Products = new List<ProductDb>
                {
                    new ProductDb { Id = 1, CurrencyId = 1, ProductTypeId = 1, Price = 1000 }
                }
            };

            _sqlDbContext.Add(orderDb);
            _sqlDbContext.SaveChanges();

            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Price = 500,
                    Currency = CurrencyType.ARS,
                    Type = ProductType.Mouse
                }
            };
            var order = new Order(products);

            _updateOrderRepository.Execute(order);

            var orderUpdated = _sqlDbContext.Orders.First(x => x.Id == orderDb.Id);

            Assert.Equal(order.TotalPrice, orderUpdated.TotalPrice);
        }

        [Fact]
        public void whenOrderNotFound_mustReturnEmptyList()
        {
            var orderDb = new OrderDb
            {
                Id = 1,
                TotalPrice = 1000,
                Products = new List<ProductDb>
                {
                    new ProductDb { Id = 1, CurrencyId = 1, ProductTypeId = 1, Price = 1000 }
                }
            };

            _sqlDbContext.Add(orderDb);
            _sqlDbContext.SaveChanges();

            var products = new List<Product> 
            { 
                new Product 
                { 
                    Id = 2, 
                    Price = 500,
                    Currency = CurrencyType.ARS,
                    Type = ProductType.Mouse
                } 
            };
            var order = new Order(products);

            Assert.ThrowsAsync<RepositoryException>(
                () => _updateOrderRepository.Execute(order));
        }
    }
}
