using API.Application.Commands;
using API.RestApi.Exceptions;
using API.RestApi.Installers;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services
.AddControllers()
.AddFluentValidation(fv =>
{
    fv.ConfigureClientsideValidation(enabled: false);
    fv.ImplicitlyValidateChildProperties = true;
    fv.RegisterValidatorsFromAssemblyContaining<RegisterUserValidator>();
});
builder.Services
.InstallServicesFromAssembly(builder.Configuration);

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseCors();
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();