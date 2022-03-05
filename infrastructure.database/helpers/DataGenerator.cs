using domain.enums;
using domain.utils;
using infrastructure.database.abstractions;
using infrastructure.database.model;
using Microsoft.Extensions.DependencyInjection;

namespace infrastructure.database.helpers
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider) 
        {
            var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ISqlDbContext>();

            dbContext.Users.Add(new UserDb { Name = "Lautaro", Password = Encrypt.EncryptString("123", "QWER") });
            dbContext.SaveChanges();

            var random = new Random();

            if (!dbContext.Products.Any())
            {
                for (int i = 0; i < 100; i++)
                {
                    dbContext.Products.Add(new ProductDb 
                    {
                        CurrencyId = 1,
                        Price = random.Next(100, 5000),
                        ProductTypeId = random.Next(1, 5)
                    });
                }

                dbContext.SaveChanges();
            }

            var quantityOfProducts = dbContext.Products.Count();

            int number = 0;
            if (!dbContext.Orders.Any())
            {
                for (int i = 0; i < 50; i++)
                {
                    var productsToInsert = dbContext.Products.Skip(number).Take(2).ToList();

                    var order = new OrderDb
                    {
                        Products = productsToInsert,
                        TotalPrice = productsToInsert.Sum(x => x.Price)
                    };

                    dbContext.Orders.Add(
                        order
                    );

                    number += 2;
                }

                dbContext.SaveChanges();
            }
        }
    }
}
