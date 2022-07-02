
namespace API.Domain;
public interface IInstaller
{
    void InstallServices(IServiceCollection services, IConfiguration config);
}