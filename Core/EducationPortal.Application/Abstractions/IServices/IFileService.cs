using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Abstractions.IServices
{
    public interface IFileService
    {
        public Tuple<int, string> SaveImage(IFormFile imageFile);
        Task DeleteImage(string imageFileName);
    }
}
