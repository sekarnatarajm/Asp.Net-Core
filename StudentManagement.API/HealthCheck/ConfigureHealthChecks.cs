using Microsoft.Extensions.DependencyInjection;

namespace StudentManagement.API.HealthCheck
{
    public static class ConfigureHealthChecks
    {
        public static void ConfigureHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer("Server=localhost,1433;Database=CustomerSettings;User Id=sa;Password=Swiftdezire@123;");


        }
    }
}
