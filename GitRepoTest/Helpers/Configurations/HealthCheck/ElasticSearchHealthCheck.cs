using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GitRepoTest.Helpers.Configurations.HealthCheck
{
    public class ElasticSearchHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {

            throw new NotImplementedException();
        }
    }
}
