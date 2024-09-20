using EducationPortal.Domain.Entities.Common;
using EducationPortal.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EducationPortal.Domain.Entities
{

    public class Course : BaseEntity
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CourseLevel CourseLevel { get; set; }
        public int CategoryId { get; set; }//FK 


       // - - - - - - - - - - - - - - - - ILISKILER - - - - - - - - - - - - - - - - - - - - - - - - -


        public Category Category { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; }

    }
}
