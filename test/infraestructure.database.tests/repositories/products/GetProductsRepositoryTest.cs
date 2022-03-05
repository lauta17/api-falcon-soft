using AutoMoqCore;
using domain.repositories.products;
using infraestructure.database.tests.helpers;
using infrastructure.database;
using infrastructure.database.mappers;
using infrastructure.database.model;
using infrastructure.database.repositories.products;
using System;
using System.Collections.Generic;
using Xunit;

namespace infraestructure.database.tests.repositories.products
{
    public class GetProductsRepositoryTest
    {
        private readonly AutoMoqer _autoMoq;
        private  SqlDbContext _sqlDbContext;

        private readonly IGetProductsRepository _getProductsRepository;

        public GetProductsRepositoryTest() 
        {
            _sqlDbContext = DataBase.Initialize();

            _autoMoq = new AutoMoqer();
            _getProductsRepository = new GetProductsRepository(_sqlDbContext, _autoMoq.Resolve<ProductMapper>());
        }

        [Fact]
        public void whenProductsFound_mustReturnIt()
        {
            _sqlDbContext.Database.EnsureDeleted();
            _sqlDbContext.Database.EnsureCreated();

            List<ProductDb> productsDb = new List<ProductDb> 
            {
                new ProductDb { Id = 1, Price = 1500, CurrencyId = 1, ProductTypeId = 1 },
                new ProductDb { Id = 2, Price = 1200, CurrencyId = 1, ProductTypeId = 1 }
            };

            _sqlDbContext.Products.AddRange(productsDb);
            _sqlDbContext.SaveChanges();

            var result = _getProductsRepository.Execute().Result;

            Assert.Equal(productsDb.Count, result.Count);

            for (int i = 0; i < productsDb.Count; i++)
            {
                Assert.Equal(productsDb[i].Id, result[i].Id);
                Assert.Equal(productsDb[i].Price, result[i].Price);
                Assert.Equal(productsDb[i].CurrencyId, (int)result[i].Currency);
                Assert.Equal(productsDb[i].ProductTypeId, (int)result[i].Type);
            }
        }

        [Fact]
        public void whenProductsNotFound_mustReturnEmptyList()
        {
            var result = _getProductsRepository.Execute().Result;

            Assert.Empty(result);
        }
    }
}
