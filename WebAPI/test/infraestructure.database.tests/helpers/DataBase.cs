using infrastructure.database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;

namespace infraestructure.database.tests.helpers
{
    public static class DataBase
    {
        public static SqlDbContext Initialize()
        {
            var _contextOptions = new DbContextOptionsBuilder<SqlDbContext>()
                                        .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                        .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                                        .Options;

            SqlDbContext sqlDbContext = new SqlDbContext(_contextOptions);

            sqlDbContext.Database.EnsureDeleted();
            sqlDbContext.Database.EnsureCreated();

            return sqlDbContext;
        }

        //public static void Clear() 
        //{
        //    sqlDbContext.Database.EnsureDeleted();
        //    sqlDbContext.Database.EnsureCreated();
        //}
    }
}
