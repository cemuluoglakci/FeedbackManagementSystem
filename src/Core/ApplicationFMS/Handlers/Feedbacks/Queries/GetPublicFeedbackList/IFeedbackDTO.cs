using ApplicationFMS.Helpers.Mappings;
using AutoMapper;
using CoreFMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetFeedbackList
{
    public interface IFeedbackDTO //: IMapFrom<Feedback>
    {
        int Id { get; set; }
        string Title { get; set; }
        string Text { get; set; }
        int? SectorId { get; set; }
        string SectorName { get; set; }
        int? CompanyId { get; set; }
        string CompanyName { get; set; }
        int? ProductId { get; set; }
        string ProductName { get; set; }
        int TypeId { get; set; }
        string TypeName { get; set; }
        int? SubTypeId { get; set; }
        string SubTypeName { get; set; }

        DateTime CreatedAt { get; set; }


    }
}
