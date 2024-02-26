namespace N5.Infrastructure
{
    using Microsoft.Extensions.DependencyInjection;
    using N5.Business.Interfaces.DataAccess;
    using N5.Business.Interfaces.Services;
    using N5.Infrastructure.DataAccess;
    using N5.Infrastructure.Services;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<N5Context>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IGroupService,GroupService>();

            return services;
        }
    }
}