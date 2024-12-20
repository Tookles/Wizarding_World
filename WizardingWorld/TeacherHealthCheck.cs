using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;
using WizardingWorld.Models.Entity;
namespace WizardingWorld
{
    internal class TeacherHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var jsonFilePath = @"Resources/teachers.json";
            var jsonData = await File.ReadAllTextAsync(jsonFilePath);
            var teachersData = JsonSerializer.Deserialize<List<Teacher>>(jsonData);
            int teachers = teachersData.Count;
            if (teachers > 0)
            {
                return HealthCheckResult.Healthy($"There are {teachers} products available.");
            }
            else
            {
                return HealthCheckResult.Unhealthy($"There are {teachers} products available.");
            }
        }
    }
}