using EducationPortal.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Features.UserAuth.Commands.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandResponse
    {
        public TokenDto Token { get; set; }
    }
}
