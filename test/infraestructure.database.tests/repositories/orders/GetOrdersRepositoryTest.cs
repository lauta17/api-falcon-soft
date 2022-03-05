using AutoMoqCore;
using domain.dtos;
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
    public class GetOrdersRepositoryTest
    {
        private readonly AutoMoqer _autoMoq;
        private readonly SqlDbContext _sqlDbContext;

        private readonly IGetOrdersRepository _getOrdersRepository;

        public GetOrdersRepositoryTest()
        {
            _sqlDbContext = DataBase.Initialize();

            _autoMoq = new AutoMoqer();
            _getOrdersRepository = new GetOrdersRepository(_sqlDbContext, new OrderMapper(_autoMoq.Resolve<ProductMapper>()));
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

            PaginationDto paginationDto = new PaginationDto();
            OrderFiltersDto ordersFilterDt = new OrderFiltersDto();

            var result = _getOrdersRepository.Execute(paginationDto, ordersFilterDt).Result;

            for (int i = 0; i < result.Count; i++)
            {
                Assert.Equal(orderDb.Id, result[i].Id);
                Assert.Equal(orderDb.TotalPrice, result[i].TotalPrice);
            }
        }

        [Fact]
        public void whenOrdersNotFound_mustReturnEmptyList()
        {
            PaginationDto paginationDto = new PaginationDto();
            OrderFiltersDto ordersFilterDt = new OrderFiltersDto();

            var result = _getOrdersRepository.Execute(paginationDto, ordersFilterDt).Result;

            Assert.Empty(result);
        }
    }
}
