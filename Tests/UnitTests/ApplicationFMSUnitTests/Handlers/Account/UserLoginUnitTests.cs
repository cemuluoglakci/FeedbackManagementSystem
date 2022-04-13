using ApplicationFMS.Handlers.Account.Queries.UserLogin;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using ApplicationFMSUnitTests.Arrange;
using AutoMapper;
using InfrastructureFMSDB;
using Microsoft.Extensions.Options;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ApplicationFMSUnitTests.Handlers.Account
{
    [Collection("QueryCollection")]
    public class UserLoginUnitTests
    {
        private readonly FMSDataContext _context;
        private readonly IMapper _mapper;
        private readonly JwtSetting _jwtSettings;

        public UserLoginUnitTests(QueryTestBase testBase)
        {
            _context = testBase.Context;
            _mapper = testBase.Mapper;
            _jwtSettings = testBase.JwtSettings;
        }
        [Fact]
        public async Task LoginUserCommandHandler_WhenCredientialsCorrect_ShouldReturnJWT()
        {
            //Arrange
            var sut = new UserLoginQueryHandler(_context, (IOptions<JwtSetting>)_jwtSettings);
            UserLoginQuery userLoginQuery = new UserLoginQuery()
            {
                Email = "johnsmith@gmail.com",
                Password = "P@ssw0rd"
            };

            //Act
            var result = await sut.Handle(userLoginQuery, CancellationToken.None);

            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<BaseResponse<string>>();
            result.data.ShouldNotBeNull();
            result.data.ShouldBeOfType<string>();

        }


    }
}
