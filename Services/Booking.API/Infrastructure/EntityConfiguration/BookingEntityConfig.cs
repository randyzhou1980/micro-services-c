using Booking.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.API.Infrastructure.EntityConfiguration
{
    public class BookingEntityConfig : IEntityTypeConfiguration<Model.Booking>
    {
        public void Configure(EntityTypeBuilder<Model.Booking> builder)
        {
            builder.ToTable("Booking");

            builder.HasKey(bi => bi.Id);

            builder.Property(bi => bi.Id)
               .ForSqlServerUseSequenceHiLo("booking_hilo")
               .IsRequired();

            builder.Property(bi => bi.TripDate)
                .IsRequired();

            builder.Property(bi => bi.FlightId)
                .IsRequired();

            builder.Property(bi => bi.TotalPassenger)
                .IsRequired();

            builder.HasMany(bi => bi.BookingDetails)
                .WithOne()
                .HasForeignKey(bd => bd.BookingId);
        }

    }
}
