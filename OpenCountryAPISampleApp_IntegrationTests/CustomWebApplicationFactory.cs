using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenCountryAPISampleApp.EFModels.UsersModel;
using System;
using System.Linq;

namespace OpenCountryAPISampleApp_IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<TestStartup>
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                       .ConfigureWebHostDefaults(webBuilder =>
                       {
                           webBuilder.UseStartup<TestStartup>()
                                     .ConfigureServices(services =>
                                     {
                                         // Remove the existing DbContext configuration
                                         var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<UsersDbContext>));
                                         if (descriptor != null)
                                         {
                                             services.Remove(descriptor);
                                         }

                                         // Add a database context using an in-memory database for testing.
                                         services.AddDbContext<UsersDbContext>(options => options.UseInMemoryDatabase("TestDb"));

                                         // Build an intermediate service provider and use it to instantiate the database context
                                         var sp = services.BuildServiceProvider();
                                         using (var scope = sp.CreateScope())
                                         {
                                             var scopedServices = scope.ServiceProvider;
                                             var context = scopedServices.GetRequiredService<UsersDbContext>();
                                             var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory>>();

                                             // Ensure the database is created.
                                             context.Database.EnsureCreated();

                                             try
                                             {
                                                 // Seed the database with test data.
                                                 SeedTestData(context);
                                             }
                                             catch (Exception ex)
                                             {
                                                 logger.LogError(ex, "An error occurred seeding the database.");
                                             }
                                         }
                                     });
                       });
        }

        public static void SeedTestData(UsersDbContext context)
        {
            var users = new List<User>
            {
                new User { Email = "test1@email.com", Name = "Test User 1", Password = "$argon2id$v=19$m=65536,t=2,p=4$MDZyeHpKU1Y1N2lmT1NUSg$lIV1H/7X2VbSD0/NItJfZ/PIXGY" },
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
