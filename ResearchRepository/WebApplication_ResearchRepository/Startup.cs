using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResearchRepository.Infrastructure.Authentication;
using Application;
using ResearchRepository.Application;
using ResearchRepository.Infrastructure;
using MudBlazor.Services;
using Radzen;
using Microsoft.AspNetCore.Identity;
using ResearchRepository.Infrastructure.Authentication;
using ResearchRepository.Application.Contacts;

namespace WebApplication_ResearchRepository
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddInfrastructureLayer(Configuration.GetConnectionString("DefaultConnection"), Configuration.GetConnectionString("authenticationConnectionString"));
            services.AddApplicationLayer();
            //mudblazor
            services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = MudBlazor.Defaults.Classes.Position.BottomRight;
            });
            //Radzen
            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<TooltipService>();
            services.AddScoped<ContextMenuService>();
            services.Configure<AuthMessageSenderOptions>(Configuration.GetSection("ApiKey"));
            services.Configure<ContactSenderOptions>(Configuration.GetSection("ApiKey"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //var serviceProvider = app.ApplicationServices.GetService<IServiceProvider>(); 
            //CreateRoles(serviceProvider).Wait();
            app.UseMiddleware<BlazorCookieLoginMiddleware<IdentityUser>>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
        
    }
}