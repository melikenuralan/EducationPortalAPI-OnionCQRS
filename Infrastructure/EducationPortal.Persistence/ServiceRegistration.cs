using EducationPortal.Application.Abstractions.IBackgroundServices;
using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Abstractions.Repositories;
using EducationPortal.Application.Abstractions.Services;
using EducationPortal.Domain.Entities;
using EducationPortal.Persistance.Contexts;
using EducationPortal.Persistence.BackgroundJobs;
using EducationPortal.Persistence.Repositories;
using EducationPortal.Persistence.Repositories.Notifications;
using EducationPortal.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace EducationPortal.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EduPortalDbContext>(opt =>
             opt.UseMySql(configuration.GetConnectionString("DefaultConnection"),
              ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))));

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<EduPortalDbContext>();

            services.AddScoped<ICourseReadRepository, CourseReadRepository>();
            services.AddScoped<ICourseWriteRepository, CourseWriteRepository>();
            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
            services.AddScoped<INotificationReadRepository, NotificationReadRepository>();
            services.AddScoped<INotificationWriteRepository, NotificationWriteRepository>();
            services.AddScoped<ITeamReadRepository, TeamReadRepository>();
            services.AddScoped<ITeamWriteRepository, TeamWriteRepository>();
            services.AddScoped<IReferenceCodeReadRepository, ReferenceCodeReadRepository>();
            services.AddScoped<IReferenceCodeWriteRepository, ReferenceCodeWriteRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IUserCourseService, UserCourseService>();
            services.AddTransient<IFileService, FileService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<ICourseBackgroundJobs, CourseBackgroundJobs>();
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}