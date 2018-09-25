using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCentre.API.Model
{
    public class Flight
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }

        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }

        public int AvaliableSeats { get; private set; }
        public FlightModel FlightModel { get; set; }

        public void UpdateAvaliableSeats(int totalPassengerNo)
        {
            AvaliableSeats = FlightModel.Capacity - totalPassengerNo;
        }

        public Flight DeepCopy()
        {
            return new Flight()
            {
                Id = this.Id,
                Name = this.Name,
                DepartureTime = this.DepartureTime,
                ArrivalTime = this.ArrivalTime,
                DepartureCity = this.DepartureCity,
                ArrivalCity = this.ArrivalCity,
                FlightModel = new FlightModel() { Capacity = this.FlightModel.Capacity }
            };
        }
    }

    public class FlightModel
    {
        public int Capacity { get; set; }
    }
}
