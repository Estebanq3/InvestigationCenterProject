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
using Microsoft.AspNetCore.Identity;
using ResearchRepository.Infrastructure.Authentication;
using System.Threading.Tasks;

namespace IntegrationTests.Infrastructure.Authorization
{
    public class AuthorizationFactory<TStartup>
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
        public static void SeedForAuthorizationTests(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var hasher = new PasswordHasher<IdentityUser>();

            var user0 = new IdentityUser()
            {
                Id = "sebastian.monterocastro@ucr.ac.cr", // primary key
                UserName = "sebastian.monterocastro@ucr.ac.cr",
                Email = "sebastian.monterocastro@ucr.ac.cr",
                EmailConfirmed = true,
                NormalizedUserName = "TESTUSER0",
                PasswordHash = hasher.HashPassword(null, "C0ntr@sena1")
            };

            IdentityResult result = userManager.CreateAsync(user0, "C0ntr@sena1").Result;

            var user1 = new IdentityUser()
            {
                Id = "andrea.alvaradoacon@ucr.ac.cr", // primary key
                UserName = "andrea.alvaradoacon@ucr.ac.cr",
                Email = "andrea.alvaradoacon@ucr.ac.cr",
                EmailConfirmed = true,
                NormalizedUserName = "TESTUSER1",
                PasswordHash = hasher.HashPassword(null, "C0ntr@sena1")
            };

            IdentityResult result1 = userManager.CreateAsync(user1, "C0ntr@sena1").Result;

            var user2 = new IdentityUser()
            {
                Id = "greivin.sanchezgarita@ucr.ac.cr", // primary key
                UserName = "greivin.sanchezgarita@ucr.ac.cr",
                Email = "greivin.sanchezgarita@ucr.ac.cr",
                EmailConfirmed = false,
                NormalizedUserName = "TESTUSER2",
                PasswordHash = hasher.HashPassword(null, "C0ntr@sena1")
            };

            IdentityResult result2 = userManager.CreateAsync(user2, "C0ntr@sena1").Result;

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
                    d => d.ServiceType == typeof(DbContextOptions<AccountsDbContext>));

                if (dbContextOptionsDescriptor != null)
                    services.Remove(dbContextOptionsDescriptor);

                var sp = services.BuildServiceProvider();
                var configuration = sp.GetRequiredService<IConfiguration>();

                services.AddDbContext<AccountsDbContext>(
                    options =>
                    {

                        options.UseSqlServer(configuration.GetConnectionString("TestConnection"));
                    });

                sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<AccountsDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<AuthorizationFactory<TStartup>>>();

                    _configuration = configuration;

                    try
                    {
                        //db.Database.;
                        SeedDatabaseForTests(db);
                        SeedForAuthorizationTests(scopedServices);
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

