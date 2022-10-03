using API.Application.Commands;
using AWSApiGateways.Config;
using AWSApiGateways.Exceptions;
using FluentValidation.AspNetCore;

namespace AWSApiGateways;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    [Obsolete]
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
        .AddFluentValidation(fv =>
        {
            fv.ConfigureClientsideValidation(enabled: false);
            fv.ImplicitlyValidateChildProperties = true;
            fv.RegisterValidatorsFromAssemblyContaining<RegisterUserValidator>();
        });

        services.InstallServicesFromAssembly(Configuration);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();
        app.UseCors();
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseHttpsRedirection();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}