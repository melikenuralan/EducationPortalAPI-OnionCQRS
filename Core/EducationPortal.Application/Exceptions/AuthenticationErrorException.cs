﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Exceptions
{
    public class AuthenticationErrorException : Exception
    {
        public AuthenticationErrorException() : base("Authentication error !") 
        {
        }

        public AuthenticationErrorException(string? message) : base(message)
        {
        }

        public AuthenticationErrorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

       
    }
}
