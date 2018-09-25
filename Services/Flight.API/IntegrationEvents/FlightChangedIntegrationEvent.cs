using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flight.API.IntegrationEvents
{
    public class FlightChangedIntegrationEvent : IntegrationEvent
    {
        public int FlightId { get; set; }
        public int Capacity { get; set; }
    }
}
