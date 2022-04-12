using ApplicationFMS.Handlers.Account.Commands.RegisterUser;
using ApplicationFMS.Interfaces;
using ApplicationFMSUnitTests.Arrange;
using CoreFMS.Entities;
using MediatR;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ApplicationFMSUnitTests.Handlers
{
    public class AccountCommandsUnitTests : TestBase
    {
        private RegisterUserCommandHandler _handler;
        private RegisterUserCommand _request;
        //private Mock<IFMSDataContext> _dataContextMock;
        //private Mock<IMediator> _mediatorMock;

        public AccountCommandsUnitTests()
        {
            //Arrange
            _request = new RegisterUserCommand()
            {
                BirthDate = DateTime.Now.AddYears(-30),
                CityId = 1,
                CompanyId = 1,
                EducationId = 1,
                Email = "testmail@gmail.com",
                FirstName = "Jack",
                LastName = "Brown",
                Password = "P@ssw0rd",
                Phone = "5551234567",
                RoleId = 1
            };

            //_dataContextMock = new Mock<IFMSDataContext>();
            //_mediatorMock = new Mock<IMediator>();
            _handler = new RegisterUserCommandHandler(_context);
        }
        [Fact]
        public async Task RegisterUserCommandHandler_WhenRequestValid_ShouldSaveUser()
        {
            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            //_dataContextMock.Verify(d => d.User.Add(It.IsAny<User>()), Times.Once);
            //_dataContextMock.Verify(d => d.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            result.Email.ShouldBe(_request.Email);
            result.FirstName.ShouldBe(_request.FirstName);
            result.LastName.ShouldBe(_request.LastName);
            result.CompanyId.ShouldBe(_request.CompanyId);
            result.EducationId.ShouldBe(_request.EducationId);
            result.CityId.ShouldBe(_request.CityId);
            result.RoleId.ShouldBe(_request.RoleId);
            result.BirthDate.ShouldBe(_request.BirthDate);
            result.Phone.ShouldBe(_request.Phone);

        }

    }
}
