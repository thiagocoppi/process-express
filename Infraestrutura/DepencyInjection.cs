using Domain.Base;
using Infraestrutura.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors.Security;
using System;
using System.Linq;

namespace Infraestrutura
{
    public static class DepencyInjection
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddOpenApiDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Title = "Documentação API";
                    document.Info.Description = "Documentação da API do processamento dos arquivos OFX";
                    document.Info.Version = "v1";
                    document.Info.TermsOfService = "none";
                    document.Info.Contact = new OpenApiContact()
                    {
                        Email = "coppithiago@gmail.com",
                        Name = "Thiago Coppi",
                        Url = "https://github.com/thiagocoppi"
                    };
                };
                config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT Token"));
                config.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT Token",
                new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    Description = "Copie 'Bearer ' +  um JWT valido para o campo abaixo",
                    In = OpenApiSecurityApiKeyLocation.Header
                }));
            });
            return services;
        }

        public static void RegisterAllStores(this IServiceCollection services)
        {
            var typeInterface = typeof(IStore);
            services.AddSingleton<IProcessExpressContext, ProcessExpressContext>();

            AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(r => r.GetTypes())
                .Where(r => typeInterface.IsAssignableFrom(r))
                .ToList()
                .ForEach(types =>
                {
                    var interfacesServices = types.GetInterfaces().Where(r => r.Name != typeof(IStore).Name).ToList();
                    if (interfacesServices.Count > 0)
                    {
                        foreach (var interfaceEach in interfacesServices)
                        {
                            services.AddScoped(interfaceEach, types);
                        }
                    }
                });
        }

        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3();
            return app;
        }
    }
}