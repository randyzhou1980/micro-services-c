using Booking.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.API.Model
{
    public class BookingRepository : IBookingRepository
    {
        #region Constructor
        private readonly BookingContext _context;
        public BookingRepository(BookingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        #endregion

        public async Task<IEnumerable<Booking>> GetBookingsAsync()
        {
            return await _context.Bookings
                .Include(b => b.BookingDetails)
                .ThenInclude(bd => bd.Passenger)
                .ToListAsync();
        }

        public async Task<Booking> GetBookingDetailsAsync(int bookingId)
        {
            return await _context.Bookings
                .Include(b => b.BookingDetails)
                .ThenInclude(bd => bd.Passenger)
                .FirstOrDefaultAsync(b => b.Id == bookingId);
        }

        public async Task<bool> CheckAvailabilityAsync(NewBookingInfo newBooking)
        {
            if (!IsAvaliableBooking(newBooking))
                return false;

            var booking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.FlightId == newBooking.FlightId
                    && b.TripDate == newBooking.TripDate);

            if (booking == null)
                return true;

            return booking.TotalPassenger + newBooking.Passengers.Count <= newBooking.FlightCapacity;
        }

        private bool IsAvaliableBooking(NewBookingInfo newBooking)
        {
            if (newBooking == null
                || newBooking.FlightId <= 0
                || newBooking.FlightCapacity <= 0
                || newBooking.TripDate <= DateTime.Now)
                return false;

            if (newBooking.Passengers == null
                || newBooking.Passengers.Count == 0
                || newBooking.FlightCapacity < newBooking.Passengers.Count)
                return false;

            if (newBooking.Passengers.Any(p => string.IsNullOrWhiteSpace(p.IndentityNo))
                || newBooking.Passengers.Any(p => string.IsNullOrWhiteSpace(p.FirstName))
                || newBooking.Passengers.Any(p => string.IsNullOrWhiteSpace(p.LastName)))
                return false;

            return true;
        }

        public async Task<int?> MakeBookingAsync(NewBookingInfo newBooking)
        {
            if (!await CheckAvailabilityAsync(newBooking))
                return null;

            var booking = await _context.Bookings
                .Include(b => b.BookingDetails)
                .ThenInclude(bd => bd.Passenger)
                .FirstOrDefaultAsync(b => b.FlightId == newBooking.FlightId
                    && b.TripDate == newBooking.TripDate);

            return booking == null
                ? await CreateBookingAsync(newBooking)
                : await UpdateBookingAsync(booking, newBooking);
        }

        private async Task<int?> CreateBookingAsync(NewBookingInfo newBooking)
        {
            try
            {
                var bookingDetails = new List<BookingDetail>();

                foreach (var p in newBooking.Passengers)
                {
                    bookingDetails.Add(new BookingDetail()
                    {
                        Passenger = await GetPassenger(p),
                    });
                }

                var booking = new Booking()
                {
                    BookingDetails = bookingDetails,
                    FlightId = newBooking.FlightId,
                    TripDate = newBooking.TripDate,
                    TotalPassenger = newBooking.Passengers.Count
                };

                _context.Bookings.Add(booking);

                await _context.SaveChangesAsync();

                return booking.Id;
            }
            catch (Exception ex)
            {
                //TODO:: Error Logging
                return null;
            }
        }

        private async Task<int> UpdateBookingAsync(Booking booking, NewBookingInfo newBooking)
        {
            var bookingDetails = new List<BookingDetail>();

            foreach (var p in newBooking.Passengers)
            {
                if(!booking.BookingDetails.Any(b => p.IndentityNo == b.Passenger.IndentityNo))
                {
                    bookingDetails.Add(new BookingDetail()
                    {
                        Passenger = await GetPassenger(p),
                    });
                }
            }

            if (bookingDetails.Count > 0)
            {
                bookingDetails.AddRange(booking.BookingDetails);

                booking.BookingDetails = bookingDetails;
                booking.TotalPassenger = bookingDetails.Count;

                await _context.SaveChangesAsync();
            }

            return booking.Id;
        }

        private async Task<Passenger> GetPassenger(Passenger passenger)
        {
            var existingPassenger = await GetPassengerByIdentity(passenger.IndentityNo);

            if (existingPassenger != null)
                return existingPassenger;

            return new Passenger()
            {
                IndentityNo = passenger.IndentityNo,
                FirstName = passenger.FirstName,
                LastName = passenger.LastName
            }; 
        }

        private async Task<Passenger> GetPassengerByIdentity(string indentityNo)
        {
            return await _context.Passengers
                .FirstOrDefaultAsync(p => p.IndentityNo == indentityNo);
        }
    }
}
