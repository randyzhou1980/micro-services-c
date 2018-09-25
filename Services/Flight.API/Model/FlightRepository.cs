using Flight.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flight.API.Model
{
    public class FlightRepository : IFlightRepository
    {
        #region Constructor
        private readonly FlightContext _context;
        public FlightRepository(FlightContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        #endregion

        public async Task<IEnumerable<Flight>> GetFlightsAsync()
        {
            return await _context.Flights.Include(f => f.FlightModel).ToListAsync();
        }

        public async Task<Flight> GetFlightDetailsAsync(int flightId)
        {
            return await _context.Flights
                .Include(f => f.FlightModel)
                .FirstOrDefaultAsync(f => f.Id == flightId);
        }
    }
}
