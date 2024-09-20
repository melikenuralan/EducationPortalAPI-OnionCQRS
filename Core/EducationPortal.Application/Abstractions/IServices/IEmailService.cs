using EducationPortal.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Abstractions.IServices
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
    }
}
