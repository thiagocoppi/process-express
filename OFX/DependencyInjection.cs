using Domain.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace OFX
{
    public static class DependecyInjection
    {
        public static void RegisterAllTypesOFX<T>(this IServiceCollection services)
        {
            var typeInterface = typeof(T);

            AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(r => r.GetTypes())
                .Where(r => typeInterface.IsAssignableFrom(r))
                .ToList()
                .ForEach(types =>
                {
                    var interfacesServices = types.GetInterfaces().Where(r => r.Name != typeof(IDomainService).Name).ToList();
                    if (interfacesServices.Count > 0)
                    {
                        foreach (var interfaceEach in interfacesServices)
                        {
                            services.AddScoped(interfaceEach, types);
                        }
                    }
                });
        }
    }
}