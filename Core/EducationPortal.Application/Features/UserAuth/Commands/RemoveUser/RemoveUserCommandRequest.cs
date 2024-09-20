﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Features.UserAuth.Commands.RemoveUser
{
    public class RemoveUserCommandRequest : IRequest<RemoveUserCommandReponse>
    {
        public Guid id { get; set; }
    }
}