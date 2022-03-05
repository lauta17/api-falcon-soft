using application.usecases.products;
using application.usecases.products.abstractions;
using AutoMoqCore;
using domain.entities;
using domain.enums;
using domain.repositories.products;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace application.tests.usecases.products
{
    public class GetProductsTests
    {
        private AutoMoqer _autoMoqer;
        private IGetProducts _getProducts;

        public GetProductsTests()
        {
            _autoMoqer = new AutoMoqer();
            _getProducts = _autoMoqer.Resolve<GetProducts>();
        }

        [Fact]
        public void whenProductsFound_mustReturnIt() 
        {
            var products = new List<Product>
            {
                new Product 
                {
                    Id = 1,
                    Price = 1000,
                    Currency = CurrencyType.ARS,
                    Type = ProductType.Mouse
                },
                new Product
                {
                    Id = 2,
                    Price = 550,
                    Currency = CurrencyType.ARS,
                    Type = ProductType.Keyboard
                }
            };

            var mockGetProductsRepository = _autoMoqer.GetMock<IGetProductsRepository>();
            mockGetProductsRepository.Setup(x => x.Execute())
                .Returns(Task.FromResult(products));


            var result = _getProducts.Execute().Result;

            Assert.Equal(products.Count, result.Count);
        }

        [Fact]
        public void whenProductsFound_mustEmptyList()
        {
            var mockGetProductsRepository = _autoMoqer.GetMock<IGetProductsRepository>();
            mockGetProductsRepository.Setup(x => x.Execute())
                .Returns(Task.FromResult(new List<Product>()));

            var result = _getProducts.Execute().Result;

            Assert.Empty(result);
        }
    }
}
