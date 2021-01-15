using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Infraestrutura.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Tests.Base
{
    public static class DataBase
    {
        public static void RunMigrations(IConfigurationRoot configuration)
        {
            var serviceProvider = CreateServices(configuration);
            using var scope = serviceProvider.CreateScope();

            RunMigrations(scope.ServiceProvider);
        }

        private static IServiceProvider CreateServices(IConfigurationRoot configuration)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(configuration["ConnectionStrings:DefaultConnection"])
                    .ScanIn(typeof(BaseMigration).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .Configure<RunnerOptions>(opt =>
                {
                    opt.Tags = new[] { "QA" };
                })
                .BuildServiceProvider(false);
        }

        private static void RunMigrations(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}