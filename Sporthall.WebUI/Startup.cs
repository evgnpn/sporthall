using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sporthall.Core;
using Sporthall.Core.Entities;
using Sporthall.Core.Identity;
using Sporthall.Core.Identity.Managers;
using Sporthall.Core.Services;
using Sporthall.Infrastructure;
using Sporthall.Infrastructure.Identity.EntityFramework.Stores;
using Sporthall.Infrastructure.Repositories.EntityFramework.Base;
using System;

namespace Sporthall.WebUI
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
            ConfigureEf(services);
            ConfigureAppServices(services);

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Home/Login";
                options.LogoutPath = "/";
                options.AccessDeniedPath = "/";
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
            });

            services.Configure<FormOptions>(a =>
            {
                a.ValueCountLimit = int.MaxValue;
            });

            services.Configure<IdentityOptions>(a =>
            {
                a.User.RequireUniqueEmail = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapAreaControllerRoute("trainingsArea", "Trainings", "Trainings/{Controller}/{Action=Index}/{id?}");
                endpoints.MapAreaControllerRoute("workersArea", "Workers", "Workers/{Controller}/{Action=Index}/{id?}");
                endpoints.MapAreaControllerRoute("managementArea", "Mgmt", "Mgmt/{Controller}/{Action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();
            });
        }

        private void ConfigureEf(IServiceCollection services)
        {
            services.AddScoped(a => new EfDbContext(new DbContextOptionsBuilder<EfDbContext>().
               UseSqlServer(Configuration.GetConnectionString("SporthallConnection")).Options));

            services.AddIdentity<User, Role>().
                AddRoleStore<EfRoleStore>().
                AddUserStore<EfUserStore>().
                AddRoleManager<AppRoleManager>().
                AddUserManager<AppUserManager>().
                AddSignInManager<SignInManager<User>>().
                AddClaimsPrincipalFactory<AppUserClaimsPrincipalFactory>().
                AddDefaultTokenProviders();

            services.AddScoped<IManualUserStore, EfManualUserStore>();
            services.AddScoped<ManualUserManager>();
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();

            ConfigureAppServices(services);
        }

        private void ConfigureAppServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHallService, HallService>();
            services.AddScoped<IHallScheduleService, HallScheduleService>();
            services.AddScoped<ISoloTrainingService, SoloTrainingService>();
            services.AddScoped<IGroupTrainingService, GroupTrainingService>();
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<ICoachService, CoachService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();

            services.AddTransient<AppServices>();
        }
    }
}
