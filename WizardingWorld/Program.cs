using WizardingWorld.Models;
using WizardingWorld.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WizardingWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();

            builder.Services.AddScoped<ISpellRepository, SpellRepository>();
            builder.Services.AddScoped<ISpellService, SpellService>();
            builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
            builder.Services.AddScoped<ITeacherService, TeacherService>();
            builder.Services.AddHealthChecks().AddCheck<TeacherHealthCheck>("teacher_health_check",
                failureStatus: HealthStatus.Unhealthy,

                tags: new[] { "file", "teacher" });
            builder.Services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter(policyName: "fixed", options =>
                {
                    options.PermitLimit = 3;
                    options.Window = TimeSpan.FromMinutes(1);
                });
                options.OnRejected = (context, cancellationToken) =>
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    return new ValueTask();
                };
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            //Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRateLimiter();

            app.UseHealthChecks("/health");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}
