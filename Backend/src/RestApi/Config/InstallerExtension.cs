using System.Reflection;
using API.Application.Settings;

namespace API.RestApi.Installers;

public static class InstallerExtension
{
    public static void InstallServicesFromAssembly(this IServiceCollection services, IConfiguration config)
    {
        var applicationTypes = Assembly.Load("API.Application").ExportedTypes; // AppDomain.CurrentDomain.GetAssemblies()
        var infrastructureTypes = Assembly.Load("API.Infrastructure").ExportedTypes;
        var domainTypes = Assembly.Load("API.Domain").ExportedTypes;


        InstallServices(applicationTypes, services, config);
        InstallServices(infrastructureTypes, services, config);
        InstallServices(domainTypes, services, config);
        InstallServices(Assembly.GetExecutingAssembly().ExportedTypes, services, config); ;
    }

    private static void InstallServices(IEnumerable<Type> assembly, IServiceCollection services, IConfiguration config)
    {
        var i = assembly.Where(x =>
            typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
        .Select(Activator.CreateInstance)
        .Cast<IInstaller>()
        .ToList();
        foreach (var installer in i)
        {
            installer.InstallServices(services, config);
        }
    }
}