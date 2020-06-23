using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raunstrup.Service.Api.Exstensions
{
    public static class ApiVersionSetupExtensionMethods
    {
        public static void AddApiVersioningAndExplorer(this IServiceCollection services)
        {
            // Add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
            // Note: the specified format code will format the version as "'v'major[.minor][-status]"
            services.AddVersionedApiExplorer(
                options => { options.GroupNameFormat = "'v'VVV"; });

            services.AddApiVersioning(
                options =>
                {
                    options.ReportApiVersions = true;

                    // Use this if you would like a new separate header to set the version in.
                    // Eg: Header 'api-version: 1.0'
                    //options.ApiVersionReader = new HeaderApiVersionReader("api-version");

                    // Use this if you would like to use the MediaType version header.
                    // Eg: Header 'Accept: application/json; v=1.0'
                    options.ApiVersionReader = new MediaTypeApiVersionReader();

                    // This is set to true so that we can set what version to select (default version)
                    // when no version has been selected.
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    // And this is where we set how to select the default version. 
                    options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
                });
        }
    }
}
