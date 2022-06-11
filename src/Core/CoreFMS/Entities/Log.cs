using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFMS.Entities
{
    public partial class Log
    {
        public int Id { get; set; }
        public string Ip { get; set; } = "0";
        public string UserEmail { get; set; } = "guest";

        public DateTime RequestDate { get; set; }
        public int RunningDuration { get; set; }
        public string EndPoint { get; set; } = string.Empty;
        public string Request { get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
    }
}
