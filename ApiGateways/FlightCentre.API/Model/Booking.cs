using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCentre.API.Model
{
    public class Booking
    {
        public int Id { get; set; }

        public DateTime TripDate { get; set; }

        public int FlightId { get; set; }
        public Flight Flight { get; set; }

        public int TotalPassenger { get; set; }

        public ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
    }

    public class BookingDetail
    {
        public int Id { get; set; }

        public Passenger Passenger { get; set; }

        public string Note { get; set; }

    }

    public class Passenger
    {
        public int Id { get; set; }
        public string IndentityNo { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PassengerName { get { return $"{FirstName} {LastName}"; } }
    }
}
