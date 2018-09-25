using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using App.Extensions;
using Flight.API.Infrastructure;
using Flight.API.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Diagnostics;
using EventBus.Base.Abstractions;

namespace Flight.API
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
            services.AddMvc()
                .AddControllersAsServices();

            services.AddSqlDbContext<FlightContext>(Configuration["ConnectionString"], 
                typeof(Startup).GetTypeInfo().Assembly.GetName().Name);

            services.AddSwaggerGen(Configuration["ApiVersion"], "Flight");

            services.AddTransient<IFlightRepository, FlightRepository>();

            services.AddRabbitMQEventBus(Configuration["SubscriptionClientName"]);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();

            app.UseSwagger(Configuration["ApiVersion"], "Flight");

            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
        }
    }
}
