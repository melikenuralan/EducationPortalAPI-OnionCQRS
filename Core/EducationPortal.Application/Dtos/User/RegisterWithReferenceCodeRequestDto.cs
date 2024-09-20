using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Dtos.User
{
    public class RegisterWithReferenceCodeRequestDto
    {
        public string ReferenceCode { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }

        // public IFormFile? ProfilePhoto { get; set; }
    }
}
