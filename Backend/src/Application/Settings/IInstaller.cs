
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Application.Settings;
public interface IInstaller
{
    void InstallServices(IServiceCollection services, IConfiguration config);
}