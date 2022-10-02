
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Domain.Settings;
public interface IInstaller
{
    void InstallServices(IServiceCollection services, IConfiguration config);
}