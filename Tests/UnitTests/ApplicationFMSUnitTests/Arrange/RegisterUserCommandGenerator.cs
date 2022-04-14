using ApplicationFMS.Handlers.Account.Commands.RegisterUser;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ApplicationFMSUnitTests.Arrange
{
    public class RegisterUserCommandGenerator : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new RegisterUserCommand
                {

                    Email = "johnsmith@gmail.com",
                    FirstName = "John",
                    LastName =  "SMITH",
                    Password =  "P@ssw0rd",
                    Phone = "123456789",
                    BirthDate = DateTime.Parse("1970-04-11 11:20:23"),
                    CityId = 1,
                    EducationId = 1,
                    RoleId = 1,
                    CompanyId = 1,
                },
                false
            };

            yield return new object[]
            {
                new RegisterUserCommand
                {

                    Email = "johnsmith@gmail.com",
                    FirstName = "John",
                    LastName =  "SMITH",
                    Password =  "P@ssw0rd",
                    Phone = "123456789",
                    BirthDate = DateTime.Parse("1970-04-11 11:20:23"),
                    CityId = 1,
                    EducationId = 1,
                    RoleId = 2,
                    CompanyId = 1,
                },
                true
            };

            yield return new object[]
            {
                new RegisterUserCommand
                {

                    Email = "johnsmith@gmail.com",
                    FirstName = "John",
                    LastName =  "SMITH",
                    Password =  "P@ssw0rd",
                    Phone = "123456789",
                    BirthDate = DateTime.Parse("1970-04-11 11:20:23"),
                    CityId = 1,
                    EducationId = 1,
                    RoleId = 3,
                    CompanyId = 1,
                },
                false
            };
            yield return new object[]
            {
                new RegisterUserCommand
                {

                    Email = "johnsmith@gmail.com",
                    FirstName = "John",
                    LastName =  "SMITH",
                    Password =  "P@ssw0rd",
                    Phone = "123456789",
                    BirthDate = DateTime.Parse("1970-04-11 11:20:23"),
                    CityId = 1,
                    EducationId = 1,
                    RoleId = 4,
                    CompanyId = 1,
                },
                false
            };
            yield return new object[]
            {
                new RegisterUserCommand
                {

                    Email = "johnsmith@gmail.com",
                    FirstName = "John",
                    LastName =  "SMITH",
                    Password =  "P@ssw0rd",
                    Phone = "123456789",
                    BirthDate = DateTime.Parse("1970-04-11 11:20:23"),
                    CityId = 1,
                    EducationId = 1,
                    RoleId = 5,
                    CompanyId = 1,
                },
                false
            };

        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
