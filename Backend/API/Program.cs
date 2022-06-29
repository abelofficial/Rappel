using API.RestApi.Exceptions;
using API.RestApi.Installers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.InstallServicesFromAssembly(builder.Configuration);

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
