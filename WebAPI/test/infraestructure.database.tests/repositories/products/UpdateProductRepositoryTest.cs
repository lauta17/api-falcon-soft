using AutoMoqCore;
using domain.entities;
using domain.exceptions;
using domain.repositories.products;
using infraestructure.database.tests.helpers;
using infrastructure.database;
using infrastructure.database.model;
using infrastructure.database.repositories.products;
using System;
using System.Linq;
using Xunit;

namespace infraestructure.database.tests.repositories.products
{
    public class UpdateProductRepositoryTest
    {
        private readonly AutoMoqer _autoMoq;
        private readonly SqlDbContext _sqlDbContext;

        private readonly IUpdateProductRepository _updateProductRepository;

        public UpdateProductRepositoryTest()
        {
            _sqlDbContext = DataBase.Initialize();

            _autoMoq = new AutoMoqer();
            _updateProductRepository = new UpdateProductRepository(_sqlDbContext);
        }

        [Fact]
        public void whenProductFound_mustUpdateIt()
        {
            var productId = 15;
            var productDb = new ProductDb 
            {
                Id = productId,
                CurrencyId = 1,
                Price = 1500,
                ProductTypeId = 3
            };

            _sqlDbContext.Products.Add(productDb);
            _sqlDbContext.SaveChanges();

            var product = new Product
            {
                Id = productId,
                Price = 1200
            };

            _updateProductRepository.Execute(product);

            var result = _sqlDbContext.Products.First(x => x.Id == productId);

            Assert.Equal(result.Price, product.Price);
        }

        [Fact]
        public void whenProductNotFound_mustThrowException()
        {
            var product = new Product
            {
                Id = 15,
                Price = 1200
            };

            Assert.ThrowsAsync<RepositoryException>(
                () => _updateProductRepository.Execute(product));
        }
    }
}
