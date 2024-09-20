using AutoMapper;
using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Abstractions.Repositories;
using EducationPortal.Application.Dtos.UserCourse;
using EducationPortal.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.Application.Features.Courses.Queries
{
    public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQueryRequest, IList<CourseDto>>
    {
        private readonly ICourseReadRepository _courseReadRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public GetAllCoursesQueryHandler(ICourseReadRepository courseReadRepository, IMapper mapper, UserManager<User> userManager, ICurrentUserService currentUserService, RoleManager<Role> roleManager)
        {
            _courseReadRepository = courseReadRepository;
            _mapper = mapper;
            _userManager = userManager;
            _currentUserService = currentUserService;
            _roleManager = roleManager;
        }
        public async Task<IList<CourseDto>> Handle(GetAllCoursesQueryRequest request, CancellationToken cancellationToken)
        {  
            var roles=_currentUserService.Roles.Contains("Admin");

            var coursesQuery = _courseReadRepository.GetAll(false).Include(c => c.Category).AsQueryable(); 

            if (!roles) coursesQuery = coursesQuery.Where(x => !x.IsDeleted);

            coursesQuery = coursesQuery.OrderBy(c => c.Id);

            var courses = await coursesQuery.ToListAsync(cancellationToken);

            var courseDtos = _mapper.Map<IList<CourseDto>>(courses);

            return courseDtos;
        }
    }
}