using AutoMapper;
using EducationPortal.Application.Dtos;
using EducationPortal.Application.Dtos.Profile;
using EducationPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Application.Mapping
{
    public class UserCourseMappingProfile : Profile
    {
        public UserCourseMappingProfile()
        {

            CreateMap<UserCourse, AssignedCourseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Course.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Course.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Course.Description))
                .ForMember(dest => dest.CourseLevel, opt => opt.MapFrom(src => src.Course.CourseLevel))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Course.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.Course.EndDate))
                .ForMember(dest => dest.IsFavorite, opt => opt.MapFrom(src => src.IsFavorite))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Course.CategoryId))
                .ForMember(dest => dest.CategoryTitle, opt => opt.MapFrom(src => src.Course.Category.Title));

            CreateMap<UserCourse, FavoriteCourseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Course.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Course.Title))
                .ForMember(dest => dest.CourseLevel, opt => opt.MapFrom(src => src.Course.CourseLevel));

            CreateMap<UserCourse, CompletedCourseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Course.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Course.Title))
                .ForMember(dest => dest.CourseLevel, opt => opt.MapFrom(src => src.Course.CourseLevel));
        }
    }
}
