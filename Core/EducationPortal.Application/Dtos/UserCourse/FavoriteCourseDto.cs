using EducationPortal.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Dtos.Profile
{
    public class FavoriteCourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public CourseLevel CourseLevel { get; set; }
    }
}
