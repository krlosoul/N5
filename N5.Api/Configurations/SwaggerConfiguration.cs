namespace N5.Api.Configurations
{
    using Microsoft.OpenApi.Models;

    public class SwaggerConfiguration
    {
        public SwaggerConfiguration(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("API N5", new OpenApiInfo
                {
                    Title = "API MANAGER N5",
                    Version = "1"
                });
                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));
            });
        }
    }
}

