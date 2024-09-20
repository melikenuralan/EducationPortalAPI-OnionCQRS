﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Features.UserAuth.Commands.AssignRoleToUser
{
    public class AssignRoleCommandRequest : IRequest<AssignRoleCommandResponse>
    {
        public Guid UserId { get; set; }
        public string[] Roles { get; set; }

    }
}