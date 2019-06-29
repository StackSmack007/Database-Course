namespace CustomAutoMapperTesting.Models
{
    using CustomAutoMapperTesting.Models.Engines;
    using CustomAutoMapperTesting.Models.People;
    using System;
    using System.Collections.Generic;
    public class Car
    {

        public Car()
        {
            RegistrationPlate = new List<char>();
            Passagers = new List<Person>();
        }

        public string Brand { get; set; }
        public bool IsInMotion { get; set; }
        public int Doors { get; set; }
        public DateTime ManifacturingDate { get; set; }
        public List<Person> Passagers { get; set; }
        public Person Driver { get; set; }

        public List<char> RegistrationPlate { get; set; }

        public List<Person> SomeShit { get; set; } = new List<Person>();//wont be mapped

        public InternalCombustionEngine Engine { get; set; }

    }
}
