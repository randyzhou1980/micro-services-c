using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.API.Model
{
    public class NewBookingInfo
    {
        public int FlightId { get; set; }
        public int FlightCapacity { get; set; }

        public DateTime TripDate { get; set; }
        public ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
    }
}
