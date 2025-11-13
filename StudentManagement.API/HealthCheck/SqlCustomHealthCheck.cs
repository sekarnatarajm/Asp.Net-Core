using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace StudentManagement.API.HealthCheck
{
    public class SqlCustomHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using SqlConnection conn = new SqlConnection("");
                conn.Open();

                string query = "SELECT 1";
                using SqlCommand cmd = new SqlCommand(query, conn);
                var resukt = (int)cmd.ExecuteScalar();
                if (resukt > 0)
                {
                    return Task.FromResult(HealthCheckResult.Healthy());
                }
                return Task.FromResult(HealthCheckResult.Unhealthy());
            }
            catch (Exception ex)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy());
            }
        }
    }
}
