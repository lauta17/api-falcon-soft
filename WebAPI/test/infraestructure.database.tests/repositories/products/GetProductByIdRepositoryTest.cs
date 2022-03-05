using AutoMoqCore;
using domain.exceptions;
using domain.repositories.products;
using infraestructure.database.tests.helpers;
using infrastructure.database;
using infrastructure.database.mappers;
using infrastructure.database.model;
using infrastructure.database.repositories.products;
using System;
using Xunit;

namespace infraestructure.database.tests.repositories.products
{
    public class GetProductByIdRepositoryTest
    {
        private readonly AutoMoqer _autoMoq;
        private readonly SqlDbContext _sqlDbContext;

        private readonly IGetProductByIdRepository _getProductByIdRepository;

        public GetProductByIdRepositoryTest()
        {
            _sqlDbContext = DataBase.Initialize();

            _autoMoq = new AutoMoqer();
            _getProductByIdRepository = new GetProductByIdRepository(_sqlDbContext, _autoMoq.Resolve<ProductMapper>());
        }

        [Fact]
        public void whenProductFound_mustReturnIt()
        {
            var productId = 15;
            var productDb = new ProductDb { Id = productId, Price = 1500, CurrencyId = 1, ProductTypeId = 1 };

            _sqlDbContext.Products.Add(productDb);
            _sqlDbContext.SaveChanges();

            var result = _getProductByIdRepository.Execute(productId).Result;

            Assert.Equal(productDb.Id, result.Id);
            Assert.Equal(productDb.Price, result.Price);
            Assert.Equal(productDb.CurrencyId, (int)result.Currency);
            Assert.Equal(productDb.ProductTypeId, (int)result.Type);
        }

        [Fact]
        public void whenProductNotFound_mustThrowException()
        {
            Assert.ThrowsAsync<RepositoryException>(
                () => _getProductByIdRepository.Execute(3));
        }
    }
}
