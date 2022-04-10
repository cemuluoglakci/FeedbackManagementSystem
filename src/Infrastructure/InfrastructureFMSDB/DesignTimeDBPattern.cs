using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace InfrastructureFMSDB
{
    public abstract class DesignTimeDBPattern<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        private const string ConnectionStringName = "FMSDatabase";
        private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        public TContext CreateDbContext(string[] args)
        {

            var basePath = Directory.GetCurrentDirectory() + string.Format("{0}..{0}..{0}Presentation{0}FmsAPI", Path.DirectorySeparatorChar);
            //return Create(basePath, Environment.GetEnvironmentVariable(AspNetCoreEnvironment));
            return CreateBase(basePath);
        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        private TContext CreateBase(string basePath)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                //.AddJsonFile($"appsettings.Local.json", optional: true)
                //.AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString(ConnectionStringName);

            return Create(connectionString);
        }

        private TContext CreateBase(string basePath, string environmentName)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                //.AddJsonFile($"appsettings.Local.json", optional: true)
                //.AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString(ConnectionStringName);

            return Create(connectionString);
        }

        private TContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                string _message = "Connection string " + connectionString + "was not retreived";
                throw new ArgumentException(_message);
            }

            Console.WriteLine("DesigningDB Connection string: " + connectionString);

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return CreateNewInstance(optionsBuilder.Options);
        }

    }
}
