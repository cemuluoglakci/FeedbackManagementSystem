using ApplicationFMS.Helpers.Mappings;
using AutoMapper;
using System;
using CoreFMS.Entities;

namespace ApplicationFMS.Handlers.System.Queries.GetLogs
{
    public class LogDto : IMapFrom<Log>
    {
        public int Id { get; set; }
        public string Ip { get; set; } = "0";
        public string UserEmail { get; set; } = "guest";
        public DateTime RequestDate { get; set; }
        public int RunningDuration { get; set; }
        public string EndPoint { get; set; } = string.Empty;
        public string Request { get; set; } = string.Empty;
        public bool ResponseStatus { get; set;}

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Log, LogDto>()
                .ForMember(d => d.ResponseStatus, opt =>
                {
                    opt.MapFrom(s =>!string.IsNullOrEmpty(s.Response) && !s.Response.Contains("\"SuccessStatus\":false,"));
                });
        }

    }
}
