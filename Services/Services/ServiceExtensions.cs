using Common.Helpers;
using Common.ServiceRegistrationAttributes;
using Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Services.Services
{
    public static class ServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            Type scopedRegistration = typeof(ScopedRegistrationAttribute);
            
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => p.IsDefined(scopedRegistration, false) && !p.IsInterface)
                .Select(s => new
                {
                    Implementation = s
                });


            foreach (var type in types)
            {
                if (type.Implementation.IsDefined(scopedRegistration, false))
                {
                    services.AddScoped(type.Implementation);
                }
            }
        }
    }
}
