using Flight.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flight.API.Infrastructure.EntityConfiguration
{
    public class FlightModelEntityConfig : IEntityTypeConfiguration<FlightModel>
    {
        public void Configure(EntityTypeBuilder<FlightModel> builder)
        {
            builder.ToTable("FlightModel");

            builder.HasKey(fm => fm.Id);

            builder.Property(fm => fm.Id)
               .ForSqlServerUseSequenceHiLo("flight_model_hilo")
               .IsRequired();

            builder.Property(fm => fm.Type)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(fm => fm.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(fm => fm.Capacity)
                .IsRequired();
        }
    }
}
