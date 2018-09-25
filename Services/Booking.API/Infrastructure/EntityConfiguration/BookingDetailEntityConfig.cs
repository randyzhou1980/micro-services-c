using Booking.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.API.Infrastructure.EntityConfiguration
{
    public class BookingDetailEntityConfig : IEntityTypeConfiguration<BookingDetail>
    {
        public void Configure(EntityTypeBuilder<BookingDetail> builder)
        {
            builder.ToTable("BookingDetail");

            builder.HasKey(bd => bd.Id);

            builder.Property(bd => bd.Id)
               .ForSqlServerUseSequenceHiLo("booking_detail_hilo")
               .IsRequired();

            //builder.HasOne(bd => bd.Booking)
            //    .WithMany(bi => bi.BookingDetails)
            //    .HasForeignKey(bd => bd.BookingId);
            builder.Property(bd => bd.BookingId)
                .IsRequired();

            builder.HasOne(bd => bd.Passenger)
                .WithMany()
                .HasForeignKey(bd => bd.PassengerId);
            builder.Property(bd => bd.PassengerId)
                .IsRequired();

            builder.Property(bd => bd.Note)
                .HasMaxLength(250)
                .IsRequired(false);
        }

    }
}
