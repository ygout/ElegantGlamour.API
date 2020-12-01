using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Hosting;
using ElegantGlamour.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoWrapper;
using ElegantGlamour.API.Extensions;
using Microsoft.Extensions.Logging;
using ElegantGlamour.Core.Models.Entity.Auth;
using Microsoft.AspNetCore.Identity;
using System;

namespace ElegantGlamour.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "./ElegtantGlamour.Web/dist";
            });

            services.AddDbContext<ElegantGlamourDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("ElegantGlamour.Data"))
            );
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1d);
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
                .AddEntityFrameworkStores<ElegantGlamourDbContext>()
                .AddDefaultTokenProviders();
            services.AddApplicationServices();

            services.AddSwaggerServices();

            services.AddAutoMapper(typeof(Startup));
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
                // app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseApiResponseAndExceptionWrapper(
                new AutoWrapperOptions
                {
                    IsApiOnly = false,
                    IsDebug = true,
                    ShowStatusCode = true
                }
            );

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors(o => {
                o.AllowAnyOrigin();
                o.AllowAnyHeader();
                o.AllowAnyMethod();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerDocumentation();

            // app.UseSpa(spa =>
            // {
            //     // To learn more about options for serving an Angular SPA from ASP.NET Core,
            //     // see https://go.microsoft.com/fwlink/?linkid=864501

            //     spa.Options.SourcePath = "./ElegtantGlamour.Web";

            //     if (env.IsDevelopment())
            //     {
            //         spa.UseAngularCliServer(npmScript: "start");
            //     }
            // });
        }
    }
}
