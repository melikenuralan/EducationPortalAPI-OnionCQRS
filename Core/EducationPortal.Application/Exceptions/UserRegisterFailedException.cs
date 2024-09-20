using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Exceptions
{
    public class UserRegisterFailedException : Exception
    {
        public UserRegisterFailedException() : base("Unexpected error occurred when creating a new user ! ")
        {
        }

        public UserRegisterFailedException(string? message) : base(message)
        {
        }

        public UserRegisterFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    
    }
}
