using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Report.CustomerStatistics
{
    public class CustomerStatisticsQuery : IRequest<BaseResponse<CustomerStatisticsVm>>
    {
        public int ProductId { get; set; }
        public int TypeId { get; set; }
    }
}
