using EducationPortal.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Features.UserAuth.Commands.LoginUser
{
    public class LoginUserCommandResponse
    {
        
       
    }
    public class LoginUserSuccessCommandResponse : LoginUserCommandResponse
    {
        public TokenDto Token { get; set; }
    }

    public class LoginUserErrorCommandResponse : LoginUserCommandResponse
    {
        public string Message { get; set; }
    }
}
