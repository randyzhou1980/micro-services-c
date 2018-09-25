using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace App.Extensions
{
    public static class WebHostExtension
    {
        public static IWebHost MigrateDbContext<TContext>(this IWebHost webHost, Action<TContext> seeder) where TContext : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetService<TContext>();

                context.Database.Migrate();

                seeder(context);
            }

            return webHost;
        }

    }
}
