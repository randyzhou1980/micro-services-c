using FlightCentre.API.Config;
using FlightCentre.API.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCentre.API.Services
{
    public class BookingService : BaseService, IBookingService
    {
        #region Constructor
        private readonly UrlsConfig _urls;
        public BookingService(IOptionsSnapshot<UrlsConfig> config) : base()
        {
            _urls = config.Value;
        }
        #endregion

        public async Task<IEnumerable<Booking>> GetBookingsAsync(SearchBookingRequest request)
        {
            var bookings = await GetBookingsAsync();
            if (request == null)
            {
                return bookings;
            }

            return bookings.Where(b => (string.IsNullOrWhiteSpace(request.PassengerName) || b.BookingDetails.Any(bd => bd.Passenger.PassengerName == request.PassengerName))
                    && (!request.TripDate.HasValue || request.TripDate.Value == b.TripDate));
        }

        public async Task<IEnumerable<Booking>> GetBookingsAsync(SearchFlightRequest request)
        {
            var bookings = await GetBookingsAsync();
            if (request == null)
            {
                return bookings;
            }

            return bookings.Where(b => b.TripDate >= request.StartDate && b.TripDate <= request.EndDate);
        }

        private async Task<IEnumerable<Booking>> GetBookingsAsync()
        {
            var data = await GetStringAsync(_urls.Booking + UrlsConfig.BookingOperations.GetBookings());

            return !string.IsNullOrEmpty(data) ? JsonConvert.DeserializeObject<IEnumerable<Booking>>(data) : null;
        }
    }
}
