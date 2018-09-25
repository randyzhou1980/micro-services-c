using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace App.Extensions
{
    public static class AppBuilderExtension
    {
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, string version, string apiName)
        {
            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{apiName} API {version}");
                });

            return app;
        }
    }
}
