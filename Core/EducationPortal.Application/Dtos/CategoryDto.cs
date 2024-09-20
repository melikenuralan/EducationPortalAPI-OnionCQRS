using AutoMapper;
using EducationPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool IsDeleted {  get; set; }
    }
}
