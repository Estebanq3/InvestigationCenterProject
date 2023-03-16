using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Infrastructure.PublicationContext;

namespace IntegrationTests.Infrastructure.PublicationContext
{
    public class PublicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        private IConfiguration? _configuration;

        public void SeedDatabaseForTests(DbContext dbContext)
        {
            if (dbContext is null || _configuration is null) throw new Exception("Error in config.");

            var seedScriptName = _configuration["SeedDataScript"];
            var sql = File.ReadAllText(seedScriptName);
            var connection = new SqlConnection(dbContext.Database.GetConnectionString());
            connection.Open();
            var cmd = new SqlCommand(sql, connection);
            int res = cmd.ExecuteNonQuery();
            connection.Close();
        }

        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder(Array.Empty<string>())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<TStartup>();
                    ConfigureWebHost(webBuilder);
                });
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "integrationsettings.json");
                config.AddJsonFile(filePath);
            });

            builder.ConfigureServices(services =>
            {
                var dbContextOptionsDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<PublicationDbContext>));

                if (dbContextOptionsDescriptor != null)
                    services.Remove(dbContextOptionsDescriptor);

                var sp = services.BuildServiceProvider();
                var configuration = sp.GetRequiredService<IConfiguration>();

                services.AddDbContext<PublicationDbContext>(
                    options =>
                    {

                        options.UseSqlServer(configuration.GetConnectionString("TestConnection"));
                    });

                sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<PublicationDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<PublicationFactory<TStartup>>>();

                    _configuration = configuration;

                    try
                    {
                        //db.Database.;
                        SeedDatabaseForTests(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: { Message}", ex.Message);
                    }
                }
            });
        }
    }
}