namespace Booking.API.Model
{
    public class BookingDetail
    {
        public int Id { get; set; }

        public int BookingId { get; set; }
        //public Model.Booking Booking { get; set; }

        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }

        public string Note { get; set; }
    }
}
