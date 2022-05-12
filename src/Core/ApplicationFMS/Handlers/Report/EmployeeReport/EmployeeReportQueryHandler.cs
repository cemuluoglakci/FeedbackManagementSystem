using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Report.EmployeeReport
{
    public class EmployeeReportQueryHandler : IRequestHandler<EmployeeReportQuery, BaseResponse<EmployeeReportVm>>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;
        private readonly IMapper _mapper;

        public EmployeeReportQueryHandler(IFMSDataContext context, IMapper mapper, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<BaseResponse<EmployeeReportVm>> Handle(EmployeeReportQuery request, CancellationToken cancellationToken)
        {
            if (_currentUser == null)
            {
                return BaseResponse<EmployeeReportVm>.Fail("User Identity could not defined.");
            }

            if (_currentUser.UserDetail.RoleName != Constants.CompanyManagerRole)
            {
                return BaseResponse<EmployeeReportVm>.Fail("User role is not authorized.");
            }

            var companyEmployeeListQuery = _context.User
                .Where(x => x.IsActive == true &&
                x.CompanyId == _currentUser.UserDetail.CompanyId &&
                x.Role.RoleName == Constants.CompanyEmployeeRole);

            //Transfering type to Data Transfer Object
            var dtoQuery = companyEmployeeListQuery.ProjectTo<EmployeeReportDto>(_mapper.ConfigurationProvider);
            var companyEmployees = await dtoQuery.ToListAsync(cancellationToken);

            var viewModel = new EmployeeReportVm
            {
                EmployeeReports = companyEmployees,
                Count = dtoQuery.Count()
            };

            return new BaseResponse<EmployeeReportVm>(viewModel);
        }
    }
}
