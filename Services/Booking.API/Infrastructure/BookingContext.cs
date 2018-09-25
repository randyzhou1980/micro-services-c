using Booking.API.Infrastructure.EntityConfiguration;
using Booking.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.API.Infrastructure
{
    public class BookingContext : DbContext
    {
        #region Constructor
        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PassengerEntityConfig());
            modelBuilder.ApplyConfiguration(new BookingDetailEntityConfig());
            modelBuilder.ApplyConfiguration(new BookingEntityConfig());
        }
        #endregion

        #region Properties
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<BookingDetail> BookingDetails { get; set; }
        public DbSet<Model.Booking> Bookings { get; set; }
        #endregion

        public class BookingContextDesignFactory : IDesignTimeDbContextFactory<BookingContext>
        {
            public BookingContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<BookingContext>()
                    .UseSqlServer("Server=.;Initial Catalog=FlightServices.BookingDb;Integrated Security=true");

                return new BookingContext(optionsBuilder.Options);
            }
        }
    }
}
