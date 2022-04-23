using MediatR;

namespace ApplicationFMS.Handlers.LookUp.LookupCompany
{
    public class LookupCompanyListQuery : IRequest<CompanyLisyVm>
    {
        public int? SectorId { get; set; }
    }
}
