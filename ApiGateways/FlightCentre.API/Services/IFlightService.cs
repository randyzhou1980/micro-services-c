using FlightCentre.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCentre.API.Services
{
    public interface IFlightService
    {
        Task<IEnumerable<Flight>> GetFLightsAsync(SearchBookingRequest request);
        Task<IEnumerable<Flight>> GetFLightsAsync(SearchFlightRequest request);
    }
}
