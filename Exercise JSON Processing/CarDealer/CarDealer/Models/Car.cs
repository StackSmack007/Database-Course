namespace CarDealer.Models
{
    using System.Collections.Generic;
    public class Car
    {
        public Car()
        {
            PartCars = new List<PartCar>();
            Sales = new List<Sale>();
        }
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public long TravelledDistance { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

        public virtual ICollection<PartCar> PartCars { get; set; }
    }
}