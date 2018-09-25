using Flight.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flight.API.Infrastructure.EntityConfiguration
{
    public class FlightEntityConfig : IEntityTypeConfiguration<Model.Flight> 
    {
        public void Configure(EntityTypeBuilder<Model.Flight> builder)
        {
            builder.ToTable("Flight");

            builder.HasKey(fi => fi.Id);

            builder.Property(fi => fi.Id)
               .ForSqlServerUseSequenceHiLo("flight_hilo")
               .IsRequired();
            builder.Property(fi => fi.Name)
                .IsRequired()
                .HasMaxLength(10);


            builder.HasOne(fi => fi.FlightModel)
                .WithMany()
                .HasForeignKey(fi => fi.FlightModelId);

            builder.Property(fi => fi.DepartureTime)
                .IsRequired();
            builder.Property(fi => fi.ArrivalTime)
                .IsRequired();

            builder.Property(fi => fi.DepartureCity)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(fi => fi.ArrivalCity)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
