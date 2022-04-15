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
                    IsActive = true,
                    RoleId = 1,
                    CityId = 1,
                    EducationId = 1,
                    CompanyId = 1,
                },

                new User
                {
                    Id = 2,
                    Email = "janesmith@gmail.com",
                    Salt = "CV512C//LYxtTNzgelc8lA==",
                    Hash = "rdTyBMG3CHIYZNEUlX8Dmpi7sQrjMRiQEVPbxsvmixg=", // Password = P@ssw0rd
                    FirstName = "Jane",
                    LastName = "SMITH",
                    IsActive = true,
                    RoleId = 2,
                    CityId = 2,
                    EducationId = 2,
                    CompanyId = 2,
                },

                new User
                {
                    Id = 3,
                    Email = "rdcosta3@theatlantic.com",
                    Salt = "FissGc6h4XFyzV6cCHluEg==",
                    Hash = "PEP6JdkFF1uT6OmreCiEKN1uxYab+EjOESjZOGXzgcE=", // Password = P@ssw0rd
                    FirstName = "John",
                    LastName = "SMITH",
                    IsActive = false,
                    RoleId = 3,
                    CityId = 3,
                    EducationId = 3,
                    CompanyId = 3,
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
                    IsActive = true,
                    RoleId = 4,
                    CityId = 4,
                    EducationId = 4,
                    CompanyId = 4,
                },

            });

            context.Role.AddRange(new[] {
                new Role { Id = 1, RoleName = "System Administrator" },
                new Role { Id = 2, RoleName = "Customer" },
                new Role { Id = 3, RoleName = "Company Manager" },
                new Role { Id = 4, RoleName = "Company Representative" },
                new Role { Id = 5, RoleName = "Company Employee" },
            });

            context.City.AddRange(new[] {
                new City { Id = 1, CityName = "İstanbul" },
                new City { Id = 2, CityName = "Ankara" },
                new City { Id = 3, CityName = "İzmir" },
                new City { Id = 4, CityName = "Konya" },
                new City { Id = 5, CityName = "Antalya" },
            });

            context.Company.AddRange(new[] {
                new Company { Id = 1, CompanyName = "Kunze Inc" },
                new Company { Id = 2, CompanyName = "Flatley-Schoen" },
                new Company { Id = 3, CompanyName = "Stokes Group" },
                new Company { Id = 4, CompanyName = "Grant LLC" },
                new Company { Id = 5, CompanyName = "Considine-Dooley" },
            });

            context.Education.AddRange(new[] {
                new Education { Id = 1, EducationName = "No formal education" },
                new Education { Id = 2, EducationName = "High school diploma" },
                new Education { Id = 3, EducationName = "College degree" },
                new Education { Id = 4, EducationName = "Vocational training" },
                new Education { Id = 5, EducationName = "Bachelor’s degree" },
            });

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
