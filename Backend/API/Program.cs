using API.Exceptions;
using API.Installers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.InstallServicesFromAssembly(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseOpenApi();
app.UseSwaggerUi3();
app.UseReDoc(c =>
{
    c.DocumentTitle = "Todo API";
    c.SpecUrl = "/swagger/v1/swagger.json";
});
app.UseRouting();

app.UseCors();
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
