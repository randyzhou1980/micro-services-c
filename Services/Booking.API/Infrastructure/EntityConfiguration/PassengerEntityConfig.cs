using Booking.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.API.Infrastructure.EntityConfiguration
{
    public class PassengerEntityConfig : IEntityTypeConfiguration<Passenger>
    {
        public void Configure(EntityTypeBuilder<Passenger> builder)
        {
            builder.ToTable("Passenger");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
               .ForSqlServerUseSequenceHiLo("Passenger_hilo")
               .IsRequired();

            builder.Property(p => p.IndentityNo)
                .IsRequired()
                .HasMaxLength(20);
            builder.HasAlternateKey(p => p.IndentityNo)
                .HasName("AlternateKey_IndentityNo");

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
