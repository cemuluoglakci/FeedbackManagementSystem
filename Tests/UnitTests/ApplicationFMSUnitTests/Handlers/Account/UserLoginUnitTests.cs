using ApplicationFMS.Handlers.Account.Queries.UserLogin;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using ApplicationFMSUnitTests.Arrange;
using AutoMapper;
using InfrastructureFMSDB;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Security.Claims;
using Shouldly;
using System.Buffers.Text;
using System.IdentityModel.Tokens.Jwt;
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

    }
}
