using application.usecases.orders;
using application.usecases.orders.abstractions;
using AutoMoqCore;
using domain.dtos;
using domain.entities;
using domain.enums;
using domain.repositories.orders;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;


namespace application.tests.usecases.orders
{
    public class GetFilteredOrdersTests
    {
        private AutoMoqer _autoMoqer;
        private IGetFilteredOrders _getFilteredOrders;

        public GetFilteredOrdersTests() 
        {
            _autoMoqer = new AutoMoqer();
            _getFilteredOrders = _autoMoqer.Resolve<GetFilteredOrders>();
        }

        [Fact]
        public void whenOrdersFound_mustReturnIt()
        {
            var paginationDto = new PaginationDto() { Limit = 10, Offset = 0 };
            var orderFilterDto = new OrderFiltersDto();

            var products = new List<Product>
            {
                new Product { Id = 1, Currency = CurrencyType.ARS, Price = 1000, Type = ProductType.Mouse },
                new Product { Id = 2, Currency = CurrencyType.ARS, Price = 255, Type = ProductType.Keyboard }
            };

            var orders = new List<Order> 
            {
                new Order(1, products),
                new Order(2, products),
            };

            var mockGetOrdersRepository = _autoMoqer.GetMock<IGetOrdersRepository>()
                .Setup(x => x.Execute(paginationDto, orderFilterDto))
                .Returns(Task.FromResult(orders));

            var result = _getFilteredOrders.Execute(paginationDto, orderFilterDto).Result;

            Assert.Equal(orders.Count, result.Count);

            for (int i = 0; i < orders.Count; i++)
            {

                Assert.Equal(orders[i].Id, result[i].Id);
                Assert.Equal(orders[i].TotalPrice, result[i].TotalPrice);
            }
        }

        [Fact]
        public void whenOrdersNotFound_mustReturnEmptyList()
        {
            var paginationDto = new PaginationDto() { Limit = 10, Offset = 0 };
            var orderFilterDto = new OrderFiltersDto();

            var mockGetOrdersRepository = _autoMoqer.GetMock<IGetOrdersRepository>()
                .Setup(x => x.Execute(paginationDto, orderFilterDto))
                .Returns(Task.FromResult(new List<Order>()));

            var result = _getFilteredOrders.Execute(paginationDto, orderFilterDto).Result;

            Assert.Empty(result);
        }
    }
}
