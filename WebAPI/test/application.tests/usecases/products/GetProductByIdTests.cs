using application.usecases.products;
using application.usecases.products.abstractions;
using AutoMoqCore;
using domain.entities;
using domain.enums;
using domain.repositories.products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace application.tests.usecases.products
{
    public class GetProductByIdTest
    {
        private AutoMoqer _autoMoqer;
        private IGetProductById _getProductById;

        public GetProductByIdTest()
        {
            _autoMoqer = new AutoMoqer();
            _getProductById = _autoMoqer.Resolve<GetProductById>();
        }

        [Fact]
        public void whenIdIsValid_mustReturnProduct()
        {
            var productId = 15;

            var product = new Product
            {
                Id = productId,
                Price = 1000,
                Currency = CurrencyType.ARS,
                Type = ProductType.Mouse
            };

            var mockGetProductByIdRepository = _autoMoqer.GetMock<IGetProductByIdRepository>();
            mockGetProductByIdRepository.Setup(x => x.Execute(productId))
                .Returns(Task.FromResult(product));

            var result = _getProductById.Execute(productId).Result;

            Assert.Equal(product.Id, result.Id);
            Assert.Equal(product.Price, result.Price);
            Assert.Equal(product.Currency, result.Currency);
            Assert.Equal(product.Type, result.Type);
        }

        [Fact]
        public void whenIdIsInvalid_mustThrowException() 
        {
            var productId = 0;

            Assert.ThrowsAsync<ArgumentException>(() => _getProductById.Execute(productId));
        }
    }
}
