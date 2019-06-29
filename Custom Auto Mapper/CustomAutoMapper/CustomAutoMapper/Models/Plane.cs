namespace CustomAutoMapperTesting.Models
{
    using CustomAutoMapperTesting.Models.Engines;
    using CustomAutoMapperTesting.Models.People;
    using System;
    using System.Collections.Generic;
    public class Plane
    {
        public Plane()
        {
            RegistrationPlate = new List<char>();
            Passagers = new List<Passager>();
        }

        public string Brand { get; set; }
        public bool IsInMotion { get; set; }
        public DateTime ManifacturingDate { get; set; }
        public decimal MaxAltitude { get; set; }
        public List<Passager> Passagers { get; set; }
        public Person Driver { get; set; }

        public List<char> RegistrationPlate { get; set; }

        public ReactiveEngine Engine { get; set; }
    }
}