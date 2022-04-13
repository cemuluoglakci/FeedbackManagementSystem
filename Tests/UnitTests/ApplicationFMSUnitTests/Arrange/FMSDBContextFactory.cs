using Castle.Core.Resource;
using CoreFMS.Entities;
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

            context.User.AddRange(new[]
            {
                new User {Id = 1, Email = "johnsmith@gmail.com", Salt = "FissGc6h4XFyzV6cCHluEg==", Hash = "PEP6JdkFF1uT6OmreCiEKN1uxYab+EjOESjZOGXzgcE=", FirstName = "John", LastName = "SMITH"}, // Password = P@ssw0rd
                new User {Id = 2, Email = "janesmith@gmail.com", Salt = "CV512C//LYxtTNzgelc8lA==", Hash = "rdTyBMG3CHIYZNEUlX8Dmpi7sQrjMRiQEVPbxsvmixg=", FirstName = "Jane", LastName = "SMITH"}, // Password = P@ssw0rd
            });

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
