using application.usecases.products.abstractions;
using AutoMoqCore;
using domain.entities;
using domain.enums;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using web.controllers;
using web.mappers.abstractions;
using web.mappers.products;
using web.models.products;
using Xunit;

namespace web.tests.controllers
{
    public class ProductControllerTest
    {
        private readonly AutoMoqer _autoMoqer;
        private readonly ProductController _productController;

        public ProductControllerTest() 
        {
            _autoMoqer = new AutoMoqer();
            _productController = _autoMoqer.Resolve<ProductController>();
        }

        [Fact]
        public void WhenProductByIdFound_MustReturnIt() 
        {
            var productId = 10;

            var product = new Product 
            {
                Id = productId,
                Price = 1000,
                Currency = CurrencyType.ARS,
                Type = ProductType.Keyboard
            };

            var mockGetProductById = _autoMoqer.GetMock<IGetProductById>();
            mockGetProductById.Setup(x => x.Execute(productId))
                .Returns(Task.FromResult(product));

            var productResponse = new ProductResponse
            {
                Id = product.Id,
                Price = product.Price,
                Currency = product.Currency,
                Type = product.Type
            };

            var mockProductResponseMapper = _autoMoqer.GetMock<IProductResponseMapper>();
            mockProductResponseMapper.Setup(x => x.Map(product))
                .Returns(productResponse);

            var response = _productController.Get(productId).Result as OkObjectResult;
            var productR = (ProductResponse)response.Value;

            Assert.Equal(response.StatusCode, (int)HttpStatusCode.OK);
            Assert.Equal(productResponse, productR);
        }

        [Fact]
        public void WhenProductFound_MustReturnIt()
        {
            var productId = 10;

            var products = new List<Product>
            {
                new Product 
                {
                    Id = productId,
                    Price = 1000,
                    Currency = CurrencyType.ARS,
                    Type = ProductType.Keyboard
                }
            };

            var mockGetProductById = _autoMoqer.GetMock<IGetProducts>();
            mockGetProductById.Setup(x => x.Execute())
                .Returns(Task.FromResult(products));

            var productsResponse = new List<ProductResponse>
            {
                new ProductResponse 
                {
                    Id = products[0].Id,
                    Price = products[0].Price,
                    Currency = products[0].Currency,
                    Type = products[0].Type
                }
            };

            var mockProductResponseMapper = _autoMoqer.GetMock<IProductResponseMapper>();
            mockProductResponseMapper.Setup(x => x.Map(products))
                .Returns(productsResponse);

            var response = _productController.Get().Result as OkObjectResult;
            var productsR = (List<ProductResponse>)response.Value;

            Assert.Equal(response.StatusCode, (int)HttpStatusCode.OK);
            Assert.Equal(productsResponse, productsR);
        }
    }
}
