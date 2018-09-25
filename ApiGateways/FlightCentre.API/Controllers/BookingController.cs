using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FlightCentre.API.Model;
using FlightCentre.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightCentre.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BookingController : Controller
    {
        #region Constructor
        private readonly IFlightService _flightService;
        private readonly IBookingService _bookingService;

        public BookingController(IFlightService flightService, IBookingService bookingService)
        {
            _flightService = flightService;
            _bookingService = bookingService;
        }
        #endregion

        [HttpPost]
        [Route("search")]
        [ProducesResponseType(typeof(IEnumerable<Booking>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBookingsByCriteria([FromBody]SearchBookingRequest request)
        {
            var bookings = await _bookingService.GetBookingsAsync(request);
            if(bookings == null)
            {
                return NotFound();
            }

            var flights = await _flightService.GetFLightsAsync(request);
            if (flights == null)
            {
                return NotFound();
            }

            foreach (var booking in bookings)
            {
                booking.Flight = flights.FirstOrDefault(f => f.Id == booking.FlightId);
            }

            return Ok(bookings);
        }

        [HttpPost]
        [Route("avaliability")]
        [ProducesResponseType(typeof(IEnumerable<Flight>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAvaliableBookings([FromBody]SearchFlightRequest request)
        {
            if (!IsValidRequest(request))
                return BadRequest();

            var flights = await _flightService.GetFLightsAsync(request);
            if (flights == null)
            {
                return NotFound();
            }

            var bookings = await _bookingService.GetBookingsAsync(request);

            return Ok(GenerateAvaliableBookings(request, flights, bookings));
        }

        private bool IsValidRequest(SearchFlightRequest request)
        {
            if (request.StartDate <= DateTime.Now)
                return false;

            if (request.StartDate > request.EndDate)
                return false;

            if ((request.EndDate - request.StartDate).TotalDays > 30)
                return false;

            if (request.PassengerNo <= 0)
                return false;

            return true;
        }

        private IEnumerable<Booking> GenerateAvaliableBookings(SearchFlightRequest request,
            IEnumerable<Flight> flights, IEnumerable<Booking> existingBookings)
        {
            var avaliableFlights = new List<Booking>();

            var tripDate = request.StartDate;
            while (tripDate <= request.EndDate)
            {
                foreach (var flight in flights)
                {
                    var existingBooking = GetExistingBookingForFlight(tripDate, flight.Id, existingBookings);

                    if (existingBooking == null)
                    {
                        avaliableFlights.Add(CreateAvaliableFlight(tripDate, flight));
                    }
                    else if(existingBooking.TotalPassenger + request.PassengerNo <= flight.FlightModel.Capacity)
                    {
                        var newFlight = flight.DeepCopy();
                        newFlight.UpdateAvaliableSeats(existingBooking.TotalPassenger);

                        existingBooking.Flight = newFlight;
                        avaliableFlights.Add(existingBooking);
                    }
                }

                tripDate = tripDate.AddDays(1);
            }

            return avaliableFlights;
        }

        private Booking GetExistingBookingForFlight(DateTime tripDate, int flightId, 
            IEnumerable<Booking> existingBookings)
        {
            if (existingBookings == null)
                return null;

            return existingBookings.FirstOrDefault(b => b.TripDate == tripDate
                && b.FlightId == flightId);
        }

        private Booking CreateAvaliableFlight(DateTime tripDate, Flight flight)
        {
            var newFlight = flight.DeepCopy();
            newFlight.UpdateAvaliableSeats(0);

            return new Booking()
            {
                TripDate = tripDate,
                FlightId = flight.Id,
                TotalPassenger = 0,
                Flight = newFlight
            };
        }
    }
}