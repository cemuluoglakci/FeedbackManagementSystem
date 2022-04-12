using Castle.Core.Resource;
using InfrastructureFMSDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMSUnitTests.Arrange
{
    public class FMSDBContextFactory
    {
        public static FMSDataContext Create()
        {
            var options = new DbContextOptionsBuilder<FMSDataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new FMSDataContext(options);

            context.Database.EnsureCreated();
            //context.....AddRange(new[] {
            //    new ... { Id = "", Name = "" },
            //    new ... { Id = "", Name = "" },
            //    new ... { Id = "", Name = "" },
            //});

            //context.///.Add(new ...
            //{
            //    ... = ""
            //});

            context.SaveChanges();

            return context;
        }

        public static void Destroy(FMSDataContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }


    }
}
