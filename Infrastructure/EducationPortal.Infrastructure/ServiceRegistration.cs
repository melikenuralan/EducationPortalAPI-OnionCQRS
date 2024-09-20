using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EducationPortal.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITokenProvider, TokenProvider>();       
        }
    }
}
