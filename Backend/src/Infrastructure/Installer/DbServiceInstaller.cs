using System;
using API.Application;
using API.Application.Settings;
using API.Domain.Entities;
using API.Infrastructure.Data;
using API.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace API.Infrastructure.Installers;

public class DbServicesInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration config)
    {
        services.AddScoped(typeof(IRepository<Project>), typeof(ProjectRepository<Project>));
        services.AddScoped(typeof(IRepository<Todo>), typeof(TodoRepository<Todo>));
        services.AddScoped(typeof(IRepository<SubTask>), typeof(SubtaskRepository<SubTask>));
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var isProduction = environment == Environments.Production;

        var connectionString = string.Empty;
        if (isProduction)
        {
            var conStrBuilder = new NpgsqlConnectionStringBuilder();

            conStrBuilder.Host = Environment.GetEnvironmentVariable("DbHost");
            conStrBuilder.Database = Environment.GetEnvironmentVariable("DbName");
            conStrBuilder.Username = Environment.GetEnvironmentVariable("DbUsername");
            conStrBuilder.Password = Environment.GetEnvironmentVariable("DbPassword");
            conStrBuilder.SslMode = SslMode.Require;
            conStrBuilder.TrustServerCertificate = true;

            connectionString = conStrBuilder.ConnectionString;
            services.AddDbContext<AppDbContext>(
                options => options.UseNpgsql(connectionString)
       );
        }
        else
        {
            connectionString = config.GetConnectionString("AppDbContext");
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
        }

    }
}