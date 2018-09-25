using FlightCentre.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCentre.API.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetBookingsAsync(SearchBookingRequest request);
        Task<IEnumerable<Booking>> GetBookingsAsync(SearchFlightRequest request);
    }
}
