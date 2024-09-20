using AutoMapper;
using EducationPortal.Application.Dtos;
using EducationPortal.Application.Dtos.UserCourse;
using EducationPortal.Domain.Entities;


namespace EducationPortal.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDto>()
            .ForMember(dest => dest.CategoryTitle, opt => opt.MapFrom(src => src.Category.Title));
            CreateMap<Category, CategoryDto>();
        }
    }
}
