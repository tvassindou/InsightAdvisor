using System.Reflection;
using AdvisorProject.Core.Interfaces;
using AdvisorProject.Infrastructure.Repositories;

namespace AdvisorProject.Extensions;
public static class ServiceCollectionExtensions
{

    public static void LoadDependencies(this IServiceCollection services, params Assembly[] assemblies)
    {
         services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        RegisterServices(services, assemblies);
        services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
        RegisterRepositories(services, assemblies);
    }
    private static void RegisterServices(IServiceCollection services, params Assembly[] assemblies)
    {
        var serviceTypes = assemblies.SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.Name.EndsWith("Service") && type.IsClass && !type.IsAbstract)
            .Select(type => new
            {
                Service = type.GetInterfaces().FirstOrDefault(),
                Implementation = type
            })
            .Where(x => x.Service != null);

        foreach (var type in serviceTypes)
        {
            if (type.Service != null)
            {
                services.AddTransient(type.Service, type.Implementation);
            }
        }
    }

    private static void RegisterRepositories(IServiceCollection services, params Assembly[] assemblies)
    {
        var repositoryTypes = assemblies.SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.Name.EndsWith("Repository") && type.IsClass && !type.IsAbstract)
            .Select(type => new
            {
                Service = type.GetInterfaces().FirstOrDefault(),
                Implementation = type
            })
            .Where(x => x.Service != null);

        foreach (var type in repositoryTypes)
        {
            if (type.Service != null)
            {
                services.AddTransient(type.Service, type.Implementation);
            }
        }
    }
}
