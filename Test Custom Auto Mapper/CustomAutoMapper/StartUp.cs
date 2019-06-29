using AutoMapper;
using CustomAutoMapperTesting.Models;
using CustomAutoMapperTesting.Models.Engines;
using CustomAutoMapperTesting.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomAutoMapperTesting
{
    public class StartUp
    {
        static void Main()
        {
            Person[] passagers = {
                new Person()
            {
                FirstName = "Sergei",
                LastName = "Bubka",
                Age = null
            },
                       new Person()
            {
                FirstName = "Kolio",
                LastName = "Paramov",
                Age = 55
            } };

            Person driver = new Person()
            {
                FirstName = "Josef",
                LastName = "Prust",
                Age = 34
            };

            var carEngine = new InternalCombustionEngine()
            {
                Model = "Ferarri",
                FuelType = "Diesel",
                HorsePower = 340
            };

            Car car = new Car()
            {
                Brand = "Mazda",
                Doors = 5,
                IsInMotion = true,
                RegistrationPlate = new List<char>("H2354AS4"),
                Driver = driver,
                ManifacturingDate = new DateTime(1990, 09, 06),
                Passagers = passagers.ToList(),
                Engine = carEngine
            };
            car.SomeShit.Add(driver);

            Mapper mapper = new Mapper();
            Plane makeMeAPlane = mapper.Map<Plane>(car);

        }
    }
}
