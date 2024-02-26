namespace N5.Api.Configurations
{
    using N5.Infrastructure;
    using N5.Business;

    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddInfrastructure();
            services.AddBusiness();

            return services;
        }
    }
}

