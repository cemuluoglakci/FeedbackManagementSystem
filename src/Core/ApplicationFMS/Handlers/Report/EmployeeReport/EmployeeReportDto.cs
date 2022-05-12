using ApplicationFMS.Helpers.Mappings;
using AutoMapper;
using CoreFMS.Entities;
using System.Linq;

namespace ApplicationFMS.Handlers.Report.EmployeeReport
{
    public class EmployeeReportDto : IMapFrom<User>
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public int TotalDirectedFeedbackCount { get; set; }
        public int RepliedFeedbackCount { get; set; }
        public int SolvedFeedbackCount { get; set; }
        public int ArchivedFeedbackCount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, EmployeeReportDto>()
                .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.FirstName + " " + s.LastName))
                .ForMember(d => d.TotalDirectedFeedbackCount, opt => opt.MapFrom(
                    s => s.DirectedFeedbacks.Where(x => x.IsActive).Count()))
                .ForMember(d => d.RepliedFeedbackCount, opt => opt.MapFrom(
                    s => s.DirectedFeedbacks.Where(x => x.IsActive && x.IsReplied).Count()))
                .ForMember(d => d.SolvedFeedbackCount, opt => opt.MapFrom(
                    s => s.DirectedFeedbacks.Where(x => x.IsActive && x.IsSolved).Count()))
                .ForMember(d => d.ArchivedFeedbackCount, opt => opt.MapFrom(
                    s => s.DirectedFeedbacks.Where(x => x.IsActive && x.IsArchived).Count()))
                ;
        }
    }
}
