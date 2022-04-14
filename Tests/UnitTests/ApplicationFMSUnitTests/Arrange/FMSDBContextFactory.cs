using CoreFMS.Entities;
using InfrastructureFMSDB;
using Microsoft.EntityFrameworkCore;
using System;

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
                new User
                {
                    Id = 1,
                    Email = "johnsmith@gmail.com", 
                    Salt = "FissGc6h4XFyzV6cCHluEg==", 
                    Hash = "PEP6JdkFF1uT6OmreCiEKN1uxYab+EjOESjZOGXzgcE=", // Password = P@ssw0rd
                    FirstName = "John", 
                    LastName = "SMITH", 
                    IsActive = true
                },

                new User 
                {
                    Id = 2, 
                    Email = "janesmith@gmail.com", 
                    Salt = "CV512C//LYxtTNzgelc8lA==", 
                    Hash = "rdTyBMG3CHIYZNEUlX8Dmpi7sQrjMRiQEVPbxsvmixg=", // Password = P@ssw0rd
                    FirstName = "Jane", 
                    LastName = "SMITH", 
                    IsActive = true
                },
                
                new User
                {
                    Id = 3,
                    Email = "rdcosta3@theatlantic.com",
                    Salt = "FissGc6h4XFyzV6cCHluEg==",
                    Hash = "PEP6JdkFF1uT6OmreCiEKN1uxYab+EjOESjZOGXzgcE=", // Password = P@ssw0rd
                    FirstName = "John",
                    LastName = "SMITH",
                    IsActive = false
                },

                new User
                {
                    Id = 4,
                    Email = "cloveredge@imgur.com",
                    Salt = "CV512C//LYxtTNzgelc8lA==",
                    Hash = "rdTyBMG3CHIYZNEUlX8Dmpi7sQrjMRiQEVPbxsvmixg=", // Password = P@ssw0rd
                    FirstName = "Conrad",
                    LastName = "Loveredge",
                    LastFailedLoginAt = DateTime.Now,
                    FailedLoginAttemptCount = 3,
                    IsActive = true
                },

            });

            context.Role.AddRange(new[] {
                new Role { Id = 1, RoleName = "System Administrator" },
                new Role { Id = 2, RoleName = "Customer" },
                new Role { Id = 3, RoleName = "Company Manager" },
                new Role { Id = 4, RoleName = "Company Representative" },
                new Role { Id = 5, RoleName = "Company Employee" },
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
