using CustomerAPI01;
using CustomerAPI01.Data;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<CustomerDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("CustomerDbContext"));
        });
        builder.Services.AddScoped<CustomerRepository>();

        builder.Services.AddOpenTelemetry()
        .ConfigureResource(resource => resource.AddService("CustomerAPI"))
            .WithTracing(tracerProviderBuilder => tracerProviderBuilder
                .AddAspNetCoreInstrumentation()
                .AddConsoleExporter())
            .WithMetrics(metricProviderBuilder => metricProviderBuilder
                .AddAspNetCoreInstrumentation()
                .AddConsoleExporter())
            .WithLogging(loggingBuilder => loggingBuilder
                .AddConsoleExporter());

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}