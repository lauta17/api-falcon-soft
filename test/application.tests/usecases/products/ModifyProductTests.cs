using application.usecases.products;
using application.usecases.products.abstractions;
using AutoMoqCore;
using domain.entities;
using domain.enums;
using domain.exceptions;
using domain.repositories.orders;
using domain.repositories.products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace application.tests.usecases.products
{
    public class ModifyProductTests
    {
        private AutoMoqer _autoMoqer;
        private IModifyProduct _modifyProduct;

        public ModifyProductTests()
        {
            _autoMoqer = new AutoMoqer();
            _modifyProduct = _autoMoqer.Resolve<ModifyProduct>();
        }

        [Fact]
        public void whenOrderFound_MustUpdateIt()
        {
            var orderId = 10;
            var product = new Product
            {
                Id = 1,
                Price = 100,
                Currency = CurrencyType.ARS,
                Type = ProductType.Mouse
            };

            var products = new List<Product>
            {
                product
            };

            var order = new Order(orderId, products);

            var mockGetOrderByIdRepository = _autoMoqer.GetMock<IGetOrderByIdRepository>();
            mockGetOrderByIdRepository.Setup(x => x.Execute(orderId))
                .Returns(Task.FromResult(order));

            var mockUpdateProductRepository = _autoMoqer.GetMock<IUpdateProductRepository>();
            var mockUpdateOrderRepository = _autoMoqer.GetMock<IUpdateOrderRepository>();

            _modifyProduct.Execute(orderId, product);

            mockUpdateProductRepository.Verify(x => x.Execute(product));
            mockUpdateOrderRepository.Verify(x => x.Execute(order));
        }

        [Fact]
        public void whenPriceIsLowerOrEqualsToZero_mustThrowException()
        {
            var orderId = 10;

            Assert.ThrowsAsync<ArgumentException>(
                () => _modifyProduct.Execute(orderId, new Product()));
        }

        [Fact]
        public void whenProductNotFoundInOrder_mustThrowBusinessException()
        {
            var orderId = 10;
            var product = new Product
            {
                Id = 1,
                Price = 100,
                Currency = CurrencyType.ARS,
                Type = ProductType.Mouse
            };

            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Price = 100,
                    Currency = CurrencyType.ARS,
                    Type = ProductType.Mouse
                }
            };

            var order = new Order(orderId, products);

            var mockGetOrderByIdRepository = _autoMoqer.GetMock<IGetOrderByIdRepository>();
            mockGetOrderByIdRepository.Setup(x => x.Execute(orderId))
                .Returns(Task.FromResult(order));

            Assert.ThrowsAsync<BusinessException>(
                () => _modifyProduct.Execute(orderId, product));
        }
    }
}
