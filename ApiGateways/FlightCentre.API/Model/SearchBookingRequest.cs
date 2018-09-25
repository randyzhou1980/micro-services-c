using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCentre.API.Model
{
    public class SearchBookingRequest
    {
        public string PassengerName { get; set; }
        public DateTime? TripDate { get; set; }

        public string ArrivalCity { get; set; }
        public string DepartureCity { get; set; }
        public string FlightNo { get; set; }
    }
}
