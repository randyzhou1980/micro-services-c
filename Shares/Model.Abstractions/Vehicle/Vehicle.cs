using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Abstractions
{
    public abstract class Vehicle
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }

        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
    }
}
