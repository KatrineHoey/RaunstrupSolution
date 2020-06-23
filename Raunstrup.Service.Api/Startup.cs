using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Raunstrup.Service.Domain;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Raunstrup.Service.Api.Exstensions;
using Microsoft.EntityFrameworkCore;
using Raunstrup.Service.Infrastructure.Database;

namespace Raunstrup.Service.Api
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
            services.AddDbContext<RaunstrupContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IItemService, DomainItemService>();
            services.AddScoped<ICustomerService, DomainCustomerService>();
            services.AddScoped<IContactService, DomainContactService>();
            services.AddScoped<IEmployeeService, DomainEmployeeService>();
            services.AddScoped<IOfferService, DomainOfferService>();
            services.AddScoped<IOfferAssignedItemService, DomainOfferAssignedItemService>();
            services.AddScoped<IOfferWorkingHoursService, DomainOfferWorkingHourservice>();
            services.AddScoped<IOfferDrivingService, DomainOfferDrivingSerivce>();
            services.AddScoped<IOfferUsedItemService, DomainOfferUsedItemService>();
            services.AddScoped<ICampaignSerivce, DomainCampaignService>();
          


            services.AddApiVersioningAndExplorer();
            services.AddSwaggerGeneration();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUIAndAddApiVersionEndPointBuilder(provider);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
