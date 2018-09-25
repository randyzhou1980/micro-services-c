using Model.Abstractions;

namespace Flight.API.Model
{
    public class Flight : Vehicle
    {
        public int FlightModelId { get; set; }
        public FlightModel FlightModel { get; set; }
    }
}
