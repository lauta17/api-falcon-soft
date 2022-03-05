using application.usecases.orders.abstractions;
using application.usecases.products.abstractions;
using AutoMoqCore;
using domain.dtos;
using domain.entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using web.controllers;
using web.mappers.abstractions;
using web.models;
using web.models.orders;
using web.models.products;
using Xunit;

namespace web.tests.controllers
{
    public class OrderControllerTest
    {
        private readonly AutoMoqer _autoMoqer;
        private readonly OrderController _orderController;

        public OrderControllerTest()
        {
            _autoMoqer = new AutoMoqer();
            _orderController = _autoMoqer.Resolve<OrderController>();
        }

        [Fact]
        public void WhenGetOrder_MustReturnIt() 
        {
            var queryStringPagination = new QueryStringPagination();
            var paginationDto = new PaginationDto() { Limit = 10, Offset = 0 };
            var mockQueryStringPaginationMapper = _autoMoqer.GetMock<IQueryStringPaginationMapper>();
            mockQueryStringPaginationMapper.Setup(x => x.Map(queryStringPagination))
                .Returns(paginationDto);

            var queryStringFilters = new QueryStringFilters();
            var orderFilterDto = new OrderFiltersDto();
            var mockQueryStringFilterMapper = _autoMoqer.GetMock<IQueryStringFiltersMapper>();
            mockQueryStringFilterMapper.Setup(x => x.Map(queryStringFilters))
                .Returns(orderFilterDto);

            var products = new List<Product> 
            {
                new Product
                {
                    Id = 1,
                    Price = 1500
                }
            };

            var orders = new List<Order> 
            {
                new Order(products)
            };

            var mockGetOrders = _autoMoqer.GetMock<IGetFilteredOrders>();
            mockGetOrders.Setup(x => x.Execute(paginationDto, orderFilterDto))
                .Returns(Task.FromResult(orders));

            var ordersResponse = new List<OrderResponse>
            {
                new OrderResponse
                {
                    Id = 15,
                    Products = new List<ProductResponse> 
                    {
                        new ProductResponse
                        {
                            Id = products[0].Id,
                            Price = products[0].Price
                        }
                    },
                    TotalPrice = 1500
                }
            };

            var mockOrderResponseMapper = _autoMoqer.GetMock<IOrderResponseMapper>();
            mockOrderResponseMapper.Setup(x => x.Map(orders))
                .Returns(ordersResponse);

            var response = _orderController.Get(queryStringPagination, queryStringFilters).Result as ObjectResult;


            var orderR = (List<OrderResponse>)response.Value;

            Assert.Equal(response.StatusCode, (int)HttpStatusCode.OK);
            Assert.Single(orderR);
        }

        [Fact]
        public void WhenPutProductInOrder_MustReturnOk() 
        {
            var orderId = 1;
            var productId = 5;

            var productRequest = new ProductRequest 
            {
                Price = 1000
            };

            var product = new Product
            {
                Id = productId,
                Price = productRequest.Price
            };

            var mockProductRequestMapper = _autoMoqer.GetMock<IProductRequestMapper>();
            mockProductRequestMapper.Setup(x => x.Map(productId, productRequest))
                .Returns(product);

            var mockModifyProduct = _autoMoqer.GetMock<IModifyProduct>();

            var response = _orderController.Put(orderId, productId, productRequest).Result;

            mockModifyProduct.Verify(x => x.Execute(orderId, product));
        }
    }
}
