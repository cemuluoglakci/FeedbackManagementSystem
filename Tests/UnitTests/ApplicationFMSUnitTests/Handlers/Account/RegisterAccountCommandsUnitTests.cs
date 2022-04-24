using ApplicationFMS.Handlers.Account.Commands.RegisterUser;
using ApplicationFMSUnitTests.Arrange;
using Shouldly;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ApplicationFMSUnitTests.Handlers.Account
{
    public class RegisterAccountCommandsUnitTests : CommandTestBase
    {
        private RegisterUserCommandHandler _handler;
        private RegisterUserCommand _request;
        //private Mock<IFMSDataContext> _dataContextMock;
        //private Mock<IMediator> _mediatorMock;

        public RegisterAccountCommandsUnitTests()
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
            _handler = new RegisterUserCommandHandler(_context, null);
        }
        [Fact]
        public async Task RegisterUserCommandHandler_WhenRequestValid_ShouldSaveUser()
        {
            //Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            //Assert
            //_dataContextMock.Verify(d => d.User.Add(It.IsAny<User>()), Times.Once);
            //_dataContextMock.Verify(d => d.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            result.data.Email.ShouldBe(_request.Email);
            result.data.FirstName.ShouldBe(_request.FirstName);
            result.data.LastName.ShouldBe(_request.LastName);
            result.data.CompanyId.ShouldBe(_request.CompanyId);
            result.data.EducationId.ShouldBe(_request.EducationId);
            result.data.CityId.ShouldBe(_request.CityId);
            result.data.RoleId.ShouldBe(_request.RoleId);
            result.data.BirthDate.ShouldBe(_request.BirthDate);
            result.data.Phone.ShouldBe(_request.Phone);
        }
        [Theory]
        [ClassData(typeof(RegisterUserCommandGenerator))]
        public async Task RegisterUserCommandHandler_WhenUserRoleNotCustomer_ShouldSaveUserAsPassive(RegisterUserCommand request, bool isActive)
        {
            //Act
            var result = await _handler.Handle(request, CancellationToken.None);

            //Assert
            result.data.IsActive.ShouldBe(isActive);
        }

    }
}
