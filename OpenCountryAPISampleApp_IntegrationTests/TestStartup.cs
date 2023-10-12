using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSubstitute;
using OpenCountryAPISampleApp.EFModels.UsersModel;
using OpenCountryAPISampleApp.Interfaces.IRepositories;
using OpenCountryAPISampleApp.Interfaces.IServices;
using OpenCountryAPISampleApp.Repositories;
using OpenCountryAPISampleApp.Services;
using Xunit;

namespace OpenCountryAPISampleApp_IntegrationTests
{
    public class TestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Controllers and MVC
            services.AddControllersWithViews();
            services.AddMvc();

            // HttpClient for RestCountries
            services.AddHttpClient<IRestCountriesRepository, RestCountriesRepository>();
            services.AddTransient<IRestCountriesService, RestCountriesService>();

            // in-memory database
            services.AddDbContext<UsersDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            // Authentication
            services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", options =>
                {
                    options.Cookie.Name = "UserLoginCookie";
                    options.LoginPath = "/Login";
                });

            // CORS policy 
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyMethod();
                });
            });

            // Controllers
            services.AddControllers();

            // Swagger/OpenAPI endpoints or configuration:
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Here is where routing starts
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=}/{action=Index}/{id?}");
            });
        }
    }

        
}


