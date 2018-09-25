using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCentre.API.Config
{
    public class UrlsConfig
    {
        public class FlightOperations
        {
            public static string GetFlights() => $"/api/Flight/flights";
            public static string GetItemsById(IEnumerable<int> ids) => $"/api/v1/catalog/items?ids={string.Join(',', ids)}";
        }

        public class BookingOperations
        {
            public static string GetBookings() => $"/api/Booking/bookings";
            public static string UpdateBasket() => "/api/v1/basket";
        }

        public string Flight { get; set; }
        public string Booking { get; set; }
    }
}
