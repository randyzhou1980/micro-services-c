using Flight.API.Infrastructure.EntityConfiguration;
using Flight.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flight.API.Infrastructure
{
    public class FlightContext: DbContext
    {
        #region Constructor
        public FlightContext(DbContextOptions<FlightContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new FlightModelEntityConfig());
            modelBuilder.ApplyConfiguration(new FlightEntityConfig());
        }
        #endregion

        #region Properties
        public DbSet<FlightModel> FlightModels { get; set; }
        public DbSet<Model.Flight> Flights { get; set; }
        #endregion
    }

    public class FlightContextDesignFactory : IDesignTimeDbContextFactory<FlightContext>
    {
        public FlightContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FlightContext>()
                .UseSqlServer("Server=.;Initial Catalog=FlightServices.FlightDb;Integrated Security=true");

            return new FlightContext(optionsBuilder.Options);
        }
    }
}
