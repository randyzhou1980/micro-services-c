using Flight.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flight.API.Infrastructure
{
    public class FlightContextSeed
    {
        public async Task SeedAsync(FlightContext context)
        {
            await Task.Run(async () =>
            {
                if (!context.FlightModels.Any())
                {
                    await context.FlightModels.AddRangeAsync(GetPreconfiguredFlightModels());

                    await context.SaveChangesAsync();
                }

                if (!context.Flights.Any())
                {
                    await context.Flights.AddRangeAsync(GetPreconfigedFlights());

                    await context.SaveChangesAsync();
                }
            });
        }

        private IEnumerable<FlightModel> GetPreconfiguredFlightModels()
        {
            return new List<FlightModel>()
            {
                new FlightModel(){Type="Helicopter", Name = "H001", Capacity = 5},
                new FlightModel(){Type="Helicopter", Name = "H002", Capacity = 7}
            };
        }

        private IEnumerable<Model.Flight> GetPreconfigedFlights()
        {
            return new List<Model.Flight>()
            {
                new Model.Flight(){ Name = "MS001", FlightModelId = 1, DepartureTime = new TimeSpan(9, 0, 0), ArrivalTime = new TimeSpan(11, 0, 0), DepartureCity = "Melbourne", ArrivalCity = "Sydney" },
                new Model.Flight(){ Name = "MS002", FlightModelId = 1, DepartureTime = new TimeSpan(14, 0, 0), ArrivalTime = new TimeSpan(16, 0, 0), DepartureCity = "Melbourne", ArrivalCity = "Sydney"  },
                new Model.Flight(){ Name = "MB001", FlightModelId = 2, DepartureTime = new TimeSpan(9, 0, 0), ArrivalTime = new TimeSpan(12, 0, 0), DepartureCity = "Melbourne", ArrivalCity = "Brisbane"  },
                new Model.Flight(){ Name = "MB002", FlightModelId = 2, DepartureTime = new TimeSpan(15, 0, 0), ArrivalTime = new TimeSpan(18, 0, 0), DepartureCity = "Melbourne", ArrivalCity = "Brisbane"  }
            };
        }

    }
}
