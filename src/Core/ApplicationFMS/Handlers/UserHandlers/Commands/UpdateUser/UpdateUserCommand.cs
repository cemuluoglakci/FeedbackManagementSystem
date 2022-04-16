﻿using ApplicationFMS.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.UserHandlers.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<BaseResponse<int>>
    {
        public int Id { get; set; }
        public string? Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Password { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? CityId { get; set; }
        public int? EducationId { get; set; }

    }
}