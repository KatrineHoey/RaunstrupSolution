using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.Service.Api.Exstensions
{
    public static class SwaggerGenerationSetupExtensionMethods
    {
        public static void AddSwaggerGeneration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                // Resolve the IApiVersionDescriptionProvider service
                // Note: that we have to build a temporary service provider here because one has not been created yet
                using (var serviceProvider = services.BuildServiceProvider())
                {
                    var provider = serviceProvider.GetRequiredService<IApiVersionDescriptionProvider>();

                    // Add a swagger document for each discovered API version
                    // Note: you might choose to skip or document deprecated API versions differently
                    foreach (var description in provider.ApiVersionDescriptions)
                        options.SwaggerDoc(description.GroupName, description.CreateInfoForApiVersion());
                }

                // Add a custom operation filter which sets default values if you are using a separate header for version
                //options.OperationFilter<SwaggerDefaultValues>();

                // Integrate xml comments, add this when we have come that far.
                //options.IncludeXmlComments(XmlCommentsFilePath);

                // Describe all enums as strings instead of integers.
                //options.DescribeAllEnumsAsStrings();
            });
        }

        public static OpenApiInfo CreateInfoForApiVersion(this ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = $"Test API {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = "This is the swagger base info for API.",
                Contact = new OpenApiContact { Name = "Contact Name", Email = "some@email.com" },
                //TermsOfService = "UnComment and put the URI here to the terms of service.",
                License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
            };

            if (description.IsDeprecated) info.Description += " This API version has been deprecated.";

            return info;
        }

        public static void UseSwaggerUIAndAddApiVersionEndPointBuilder(this IApplicationBuilder app,
            IApiVersionDescriptionProvider provider)
        {
            app.UseSwaggerUI(c =>
            {
                // Build a swagger endpoint for each discovered API version
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                    c.RoutePrefix = string.Empty;
                }
            });
        }
    }
}
