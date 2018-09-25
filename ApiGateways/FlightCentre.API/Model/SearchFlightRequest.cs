using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCentre.API.Model
{
    public class SearchFlightRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int PassengerNo { get; set; }
    }
}
