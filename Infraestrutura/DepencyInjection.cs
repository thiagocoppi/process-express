﻿using Domain.Base;
using Infraestrutura.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.AspNetCore;
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

        public static void RegisterDbContext(this IServiceCollection services)
        {
            services.AddSingleton<IProcessExpressContext, ProcessExpressContext>();
        }

        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3(options =>
            {
                foreach(var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerRoutes.Add(new SwaggerUi3Route(description.GroupName, $"/swagger/{description.GroupName}/swagger.json"));
                }
            });
            return app;
        }
    }
}