using Contacts.Domain.Interfaces;
using Contacts.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Contacts.Infrastructure.ServiceCollectionExtensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {        
        services.AddScoped<IContactRepository, ContactRepository>();
        return services;
    }
}
