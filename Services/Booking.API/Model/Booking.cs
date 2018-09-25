using System;
using System.Collections.Generic;

namespace Booking.API.Model
{
    public class Booking
    {
        public int Id { get; set; }

        public DateTime TripDate { get; set; }

        public int FlightId { get; set; }

        public int TotalPassenger { get; set; }

        public List<BookingDetail> BookingDetails { get; set; }
    }
}
