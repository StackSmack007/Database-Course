using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.DTO.Task_11;
using CarDealer.DTO.Task_5;
using CarDealer.DTO.Task_6;
using CarDealer.DTO.Task_8;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        private const string PrintImportResult = "Successfully imported {0}.";
        public static void Main(string[] args)
        {
            Mapper.Initialize(cfg => cfg.AddProfile<CarDealerProfile>());
            using (var context = new CarDealerContext())
            {
                // context.Database.EnsureDeleted();
                // context.Database.EnsureCreated();
                // #region Task1
                // string suppliersJson = File.ReadAllText(@"../../../Datasets/suppliers.json");
                // Console.WriteLine(ImportSuppliers(context, suppliersJson));
                // #endregion
                //
                // #region Task2
                // string partsJson = File.ReadAllText(@"../../../Datasets/parts.json");
                // Console.WriteLine(ImportParts(context, partsJson));
                // #endregion
                //
                // #region Task3
                //   string CarsJson = File.ReadAllText(@"../../../Datasets/cars.json");
                //   Console.WriteLine(ImportCars(context, CarsJson));
                // #endregion
                //
                // #region Task4
                // string customersJson = File.ReadAllText(@"../../../Datasets/customers.json");
                // Console.WriteLine(ImportCustomers(context, customersJson));
                // #endregion
                //
                // #region Task5
                //   string salesJson = File.ReadAllText(@"../../../Datasets/sales.json");
                //   Console.WriteLine(ImportSales(context, salesJson));
                // #endregion

                #region Task6
                //Console.WriteLine(GetOrderedCustomers(context));
                #endregion

                #region Task7
                // Console.WriteLine(GetCarsFromMakeToyota(context));
                #endregion

                #region Task8
                // Console.WriteLine(GetLocalSuppliers(context));
                #endregion

                #region Task9
                // Console.WriteLine(GetCarsWithTheirListOfParts(context));
                #endregion

                #region Task10
                //  Console.WriteLine(GetTotalSalesByCustomer(context));
                #endregion

                #region Task11
                Console.WriteLine(GetSalesWithAppliedDiscount(context));
                #endregion
            }
        }

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var newSuppliers = JsonConvert.DeserializeObject<Supplier[]>(inputJson);

            context.Suppliers.AddRange(newSuppliers);
            int rowsAffected = context.SaveChanges();
            return string.Format(PrintImportResult, rowsAffected);
        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            int[] supplierIdsAllowed = context.Suppliers.Select(x => x.Id).ToArray();
            var newParts = JsonConvert.DeserializeObject<Part[]>(inputJson)
                .Where(x => supplierIdsAllowed.Contains(x.SupplierId)).ToArray();

            context.Parts.AddRange(newParts);
            int rowsAffected = context.SaveChanges();
            return string.Format(PrintImportResult, rowsAffected);
        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            int[] allowedPartIds = context.Parts.Select(x => x.Id).ToArray();

            car_in_dto[] allCars = JsonConvert.DeserializeObject<car_in_dto[]>(inputJson);

            car_in_dto[] supportedCars = allCars.Where(c => c.PartIds.All(x => allowedPartIds.Contains(x))).ToArray();

            foreach (var carDTO in supportedCars)
            {
                Car newCar = Mapper.Map<Car>(carDTO);
                context.Cars.Add(newCar);

                foreach (var pID in carDTO.PartIds.Distinct())
                {
                    newCar.PartCars.Add(new PartCar()
                    {
                        PartId = pID,
                        Car = newCar,
                    });
                }
            }
            context.SaveChanges();
            return string.Format(PrintImportResult, supportedCars.Count());
        }

        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            Customer[] newCustomers = JsonConvert.DeserializeObject<Customer[]>(inputJson);
            context.Customers.AddRange(newCustomers);
            int rowsAffected = context.SaveChanges();
            return string.Format(PrintImportResult, rowsAffected);
        }

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            decimal additionalDiscountForYoungDrivers = 5m;
            int[] allowedCarIds = context.Cars.Select(x => x.Id).ToArray();
            customer_id_isYoungDTO[] allowedCustomers = context.Customers.ProjectTo<customer_id_isYoungDTO>().ToArray();
            Sale[] sales = JsonConvert.DeserializeObject<Sale[]>(inputJson)
                .Where(x => IsValidSale(x.CustomerId, x.CarId, allowedCarIds, allowedCustomers))
                .ToArray();

            int[] customersIdsOfYoungDrivers = allowedCustomers.Where(x => x.IsYoungDriver).Select(x => x.Id).ToArray();
            Sale[] salesForDiscount = sales.Where(x => customersIdsOfYoungDrivers.Contains(x.CustomerId)).ToArray();

            for (int i = 0; i < salesForDiscount.Length; i++)
            {
                salesForDiscount[i].Discount += additionalDiscountForYoungDrivers;
            }

            context.Sales.AddRange(sales);
            int rowsAffected = context.SaveChanges();

            return string.Format(PrintImportResult, rowsAffected);
        }
        private static bool IsValidSale(int customerId, int carId, int[] allowedCarIds, customer_id_isYoungDTO[] allowedCustomers)
        {
            int[] allowedCustomerIds = allowedCustomers.Select(x => x.Id).ToArray();
            return allowedCarIds.Contains(carId) && allowedCustomerIds.Contains(customerId);
        }

        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers.OrderBy(x => x.BirthDate).ThenBy(x => x.IsYoungDriver).ProjectTo<customer_DTO_out>().ToArray();
            string jsonResult = JsonConvert.SerializeObject(customers, Formatting.Indented);
            return jsonResult;
        }

        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            string MakeToSearch = "Toyota";
            car_out_dto[] cars = context.Cars.Where(x => x.Make == MakeToSearch)
                                            .OrderBy(x => x.Model).ThenByDescending(x => x.TravelledDistance)
                                            .ProjectTo<car_out_dto>()
                                            .ToArray();
            string resultJson = JsonConvert.SerializeObject(cars, Formatting.Indented);

            return resultJson;
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            supplier_DTOut[] suppliers = context.Suppliers.Where(x => !x.IsImporter)
                                             .Select(x => new supplier_DTOut()
                                             {
                                                 Id = x.Id,
                                                 Name = x.Name,
                                                 PartsCount = x.Parts.Count()
                                             })
                                             .ToArray();
            string jsonResult = JsonConvert.SerializeObject(suppliers, Formatting.Indented);
            return jsonResult;
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                              .Select(x => new
                              {
                                  car = new
                                  {
                                      x.Make,
                                      x.Model,
                                      x.TravelledDistance
                                  },
                                  parts = x.PartCars.Select(p => new
                                  {
                                      p.Part.Name,
                                      Price = string.Format("{0:F2}", p.Part.Price)
                                  }).ToArray()
                              }).ToArray();
            string jsonResult = JsonConvert.SerializeObject(cars, Formatting.Indented);
            return jsonResult;
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customerssWhoBoughtSomething = context.Customers.Where(x => x.Sales.Count > 0)
                .Select(x => new
                {
                    fullName = x.Name,
                    boughtCars = x.Sales.Count,
                    spentMoney = x.Sales.SelectMany(s => s.Car.PartCars).Select(p => p.Part).Sum(z => z.Price)
                }).OrderByDescending(x => x.spentMoney).ThenByDescending(x => x.boughtCars).ToArray();

            string jsonResult = JsonConvert.SerializeObject(customerssWhoBoughtSomething, Formatting.Indented);
            return jsonResult;
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            saleWithDiscount_dto[] sales = context.Sales.Take(10).Select(x => new saleWithDiscount_dto()
            {
                Car=new car_out_dto()
                {
                    Make=x.Car.Make,
                    Model=x.Car.Model,
                    TravelledDistance=x.Car.TravelledDistance
                },
                CustomerName=x.Customer.Name,
                DecimalPrice = x.Car.PartCars.Select(p => p.Part.Price).Sum(),
                DecimalDiscount=x.Discount,
            }).ToArray();



            string resultJson = JsonConvert.SerializeObject(sales, Formatting.Indented);
            return resultJson;
        }

    }
}