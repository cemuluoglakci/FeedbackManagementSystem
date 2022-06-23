using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Interfaces
{
    public interface ISearchQuery
    {
        int ObjectsPerPage { get; set; } 
        int PageNumber { get; set;}
        string SortColumn { get; set;}
        bool IsAscending { get; set;}
        bool? IsActive { get; set;}
    }
}
