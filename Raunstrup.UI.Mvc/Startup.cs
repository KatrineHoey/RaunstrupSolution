using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Raunstrup.UI.MVC.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Raunstrup.UI.MVC.Services;
using Microsoft.AspNetCore.Http;
using Raunstrup.Service.Contract.Services;
using Raunstrup.Service.Contract;

namespace Raunstrup.UI.MVC
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
            //Til contactController
            services.AddHttpClient<IContactService, ContactServiceProxy>(client =>
            {
                client.BaseAddress = new Uri(Configuration["BaseUrl"]);
            });

            services.AddHttpClient<ICustomerService, CustomerServiceProxy>(client =>
            {
                client.BaseAddress = new Uri(Configuration["BaseUrl"]);
            });

            services.AddHttpClient<IItemService, ItemServiceProxy>(client =>
            {
                client.BaseAddress = new Uri(Configuration["BaseUrl"]);
            });

            services.AddHttpClient<IOffeEmployeeService, EmployeeServiceProxy>(client =>
            {
                client.BaseAddress = new Uri(Configuration["BaseUrl"]);
            });

            services.AddHttpClient<IOfferService, OfferServiceProxy>(client =>
            {
                client.BaseAddress = new Uri(Configuration["BaseUrl"]);
            });


            services.AddHttpClient<IOfferAssignedItemService, OfferAssignedItemServiceProxy>(client =>
            {
                client.BaseAddress = new Uri(Configuration["BaseUrl"]);
            });

            services.AddHttpClient<IOfferWorkingHoursService, OfferWorkingHoursServiceProxy>(client =>
            {
                client.BaseAddress = new Uri(Configuration["BaseUrl"]);
            });

            services.AddHttpClient<IOfferDrivingService, OfferDrivingProxy>(client =>
            {
                client.BaseAddress = new Uri(Configuration["BaseUrl"]);
            });

            services.AddHttpClient<IOfferUsedItemService, OfferUsedItemProxy>(client =>
            {
                client.BaseAddress = new Uri(Configuration["BaseUrl"]);
            });

            services.AddHttpClient<IOfferEmployeeService, OfferEmployeeServiceProxy>(client =>
            {
                client.BaseAddress = new Uri(Configuration["BaseUrl"]);
            });

            services.AddHttpClient<ICampaignService, CampaignServiceProxy>(client =>
            {
                client.BaseAddress = new Uri(Configuration["BaseUrl"]);
            });

            //Til log ind:
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
            Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));


            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 1;
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
