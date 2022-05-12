using System.Collections.Generic;

namespace ApplicationFMS.Handlers.Report.EmployeeReport
{
    public class EmployeeReportVm
    {
        public IList<EmployeeReportDto> EmployeeReports { get; set; } = null!;

        public int Count { get; set; }
    }
}
