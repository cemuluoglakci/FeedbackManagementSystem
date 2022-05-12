using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Report.EmployeeReport
{
    public class EmployeeReportQuery : IRequest<BaseResponse<EmployeeReportVm>>
    {
    }
}
