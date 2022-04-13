using ApplicationFMS.Handlers.Account.Commands.RegisterUser;
using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using MediatR;
using Moq;
using Shouldly;
using System;
using Xunit;

namespace ApplicationFMSUnitTests.Helpers
{
    public class SecurityHelperUnitTests
    {


        [Fact]
        public void GenerateSalt_Always_ShouldReturnValue()
        {
            //Aarrange
            string salt = null;

            //Act
            salt = Security.GenerateSalt();

            //Assert
            salt.ShouldNotBeNull();
        }

        [Theory]
        [InlineData("123456")]
        [InlineData("P@ssw0rd")]
        public void SaltAndHashPassword_Always_ShouldReturnValue(string password)
        {
            //Arrange
            string result = null;

            //Act
            string salt = Security.GenerateSalt();
            result = Security.SaltAndHashPassword(password, salt);

            //Assert
            result.ShouldNotBeNull();
        }




    }
}
