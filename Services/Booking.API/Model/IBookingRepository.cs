using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.API.Model
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetBookingsAsync();
        Task<Booking> GetBookingDetailsAsync(int bookingId);
        Task<bool> CheckAvailabilityAsync(NewBookingInfo newBooking);
        Task<int?> MakeBookingAsync(NewBookingInfo newBooking);
    }
}
