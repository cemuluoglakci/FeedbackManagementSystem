using ApplicationFMS.Handlers.Account.Queries.UserLogin;
using ApplicationFMS.Models;
using ApplicationFMSUnitTests.Arrange;
using AutoMapper;
using InfrastructureFMSDB;
using Microsoft.Extensions.Options;
using Shouldly;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
        private readonly IOptions<JwtSetting> _jwtSettings;
        private readonly UserLoginQueryHandler _sut;
        private readonly UserLoginQuery _userLoginQuery;

        public UserLoginUnitTests(QueryTestBase testBase)
        {
            _context = testBase.Context;
            _mapper = testBase.Mapper;
            _jwtSettings = testBase.JwtSettingOptions;

            //Arrange
            _sut = new UserLoginQueryHandler(_context, _jwtSettings);
            _userLoginQuery = new UserLoginQuery()
            {
                Email = "johnsmith@gmail.com",
                Password = "P@ssw0rd"
            };
        }
        [Fact]
        public async Task LoginUserCommandHandler_WhenCredientialsCorrect_ShouldReturnString()
        {
            //Act
            var result = await _sut.Handle(_userLoginQuery, CancellationToken.None);

            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<BaseResponse<string>>();
            result.Meta.SuccessStatus.ShouldBeTrue();
            result.data.ShouldNotBeNull();
            result.data.ShouldBeOfType<string>();
            result.data.Split(".").Length.ShouldBe(3);
        }

        [Fact]
        public async Task LoginUserCommandHandler_WhenCredientialsCorrect_ShouldReturnValidJWT()
        {
            //Act
            var result = await _sut.Handle(_userLoginQuery, CancellationToken.None);
            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(result.data);

            //Assert
            jwtSecurityToken.GetType().Name.ToString().ShouldBe("JwtSecurityToken");
            var test = jwtSecurityToken.Claims;
            jwtSecurityToken.Claims.First(claim => claim.Type == "email").Value.ShouldBe(_userLoginQuery.Email);
        }

        [Fact]
        public async Task LoginUserCommandHandler_WhenAccountPassive_ShouldRejectLogin()
        {
            //Arrange
            UserLoginQuery userLoginQuery = new UserLoginQuery()
            {
                Email = "rdcosta3@theatlantic.com",
                Password = "P@ssw0rd"
            };

            //Act
            var result = await _sut.Handle(userLoginQuery, CancellationToken.None);

            //Assert
            result.data.ShouldBeNull();
            result.Meta.SuccessStatus.ShouldBeFalse();
            result.Meta.Message.ShouldBe("This E-mail is not registered to the system!");
        }

        [Fact]
        public async Task LoginUserCommandHandler_WhenAccountLocked_ShouldRejectLogin()
        {
            //Arrange
            UserLoginQuery userLoginQuery = new UserLoginQuery()
            {
                Email = "cloveredge@imgur.com",
                Password = "P@ssw0rd"
            };

            //Act
            var result = await _sut.Handle(userLoginQuery, CancellationToken.None);

            //Assert
            result.data.ShouldBeNull();
            result.Meta.SuccessStatus.ShouldBeFalse();
            result.Meta.Message.ShouldBe("Your account is locked, please try again later.");
        }

    }
}
