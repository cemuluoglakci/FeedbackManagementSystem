using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Report.CustomerStatistics
{
    public class CustomerStatisticsQuery : IRequest<BaseResponse>
    {
        public int ProductId { get; set; }
        public int TypeId { get; set; }
    }
}
