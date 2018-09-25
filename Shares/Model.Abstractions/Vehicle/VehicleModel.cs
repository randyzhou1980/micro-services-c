using System;

namespace Model.Abstractions
{
    public abstract class VehicleModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
    }
}
