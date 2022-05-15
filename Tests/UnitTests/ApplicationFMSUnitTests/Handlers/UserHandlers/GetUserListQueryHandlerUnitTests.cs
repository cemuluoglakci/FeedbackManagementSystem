using ApplicationFMS.Handlers.Account.Queries.UserLogin;
using ApplicationFMS.Handlers.UserHandlers.Queries.GetUserList;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using ApplicationFMSUnitTests.Arrange;
using AutoMapper;
using InfrastructureFMSDB;
using Microsoft.Extensions.Options;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static ApplicationFMSUnitTests.Arrange.QueryTestBase;

namespace ApplicationFMSUnitTests.Handlers.UserHandlers
{
    [Collection("QueryCollection")]
    public class GetUserListQueryHandlerUnitTests
    {
        private readonly FMSDataContext _context;
        private readonly IMapper _mapper;
        private readonly GetUserListQueryHandler _sut;
        private readonly GetUserListQuery _userListQuery;

        public GetUserListQueryHandlerUnitTests(QueryTestBase testBase)
        {
            //Arrange
            //CurrentUserService currentUser = new CurrentUserService
            _mapper = testBase.Mapper;
            _context = testBase.Context;
            _sut = new GetUserListQueryHandler(_context, _mapper, new TestCurrentUser("System Administrator"));
            _userListQuery = new GetUserListQuery();
        }
        [Fact]
        public async Task GetUserListQueryHandler_WhenCurrentUserIsAdmin_ShouldReturnList()
        {
            //Act
            var result = await _sut.Handle(_userListQuery, CancellationToken.None);

            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<BaseResponse>();
            result.Meta.SuccessStatus.ShouldBeTrue();
            result.data.ShouldNotBeNull();
            result.data.ShouldBeOfType<UserListVm>();
        }
    }
}
