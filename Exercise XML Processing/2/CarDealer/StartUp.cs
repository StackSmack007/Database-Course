using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarDealer.Data;
using CarDealer.Dtos.Export;
using CarDealer.Dtos.Import;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        private readonly static string importedMessage = "Successfully imported {0}";

        public static void Main(string[] args)
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new CarDealerProfile()));
            using (var context = new CarDealerContext())
            {
                //   context.Database.EnsureDeleted();
                //   context.Database.EnsureCreated();

                #region Task 9
                //   string inputStr9 = File.ReadAllText(@"../../../Datasets/suppliers.xml");
                //   System.Console.WriteLine(ImportSuppliers(context, inputStr9));
                #endregion

                #region Task 10
                //  string inputStr10 = File.ReadAllText(@"../../../Datasets/parts.xml");
                //  System.Console.WriteLine(ImportParts(context, inputStr10));

                #endregion

                #region Task 11
                // string inputStr11 = File.ReadAllText(@"../../../Datasets/cars.xml");
                // System.Console.WriteLine(ImportCars(context, inputStr11));

                #endregion

                #region Task 12
                // string inputStr12 = File.ReadAllText(@"../../../Datasets/customers.xml");
                // System.Console.WriteLine(ImportCustomers(context, inputStr12));

                #endregion

                #region Task 13
                //  string inputStr13 = File.ReadAllText(@"../../../Datasets/sales.xml");
                //  System.Console.WriteLine(ImportSales(context, inputStr13));
                #endregion

                #region Task 14
                // System.Console.WriteLine(GetCarsWithDistance(context));
                #endregion

                #region Task 15
                // System.Console.WriteLine(GetCarsFromMakeBmw(context));
                #endregion

                #region Task 16
                //  System.Console.WriteLine(GetLocalSuppliers(context));
                #endregion

                #region Task 17
                //  System.Console.WriteLine(GetCarsWithTheirListOfParts(context));
                #endregion

                #region Task 18
                // System.Console.WriteLine(GetTotalSalesByCustomer(context));
                #endregion

                #region Task 19
                 System.Console.WriteLine(GetSalesWithAppliedDiscount(context));
                #endregion

            }
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(imp_supplier_dto[]), new XmlRootAttribute("Suppliers"));

            imp_supplier_dto[] suppliersDTO = (imp_supplier_dto[])serializer.Deserialize(new StringReader(inputXml));

            Supplier[] suppliers = suppliersDTO.Select(x => Mapper.Map<Supplier>(x)).ToArray();
            context.Suppliers.AddRange(suppliers);
            int addedSuppliers = context.SaveChanges();
            return string.Format(importedMessage, addedSuppliers);
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            int[] allowedSupplierIds = context.Suppliers.Select(x => x.Id).ToArray();
            var serializer = new XmlSerializer(typeof(imp_part_dto[]), new XmlRootAttribute("Parts"));

            imp_part_dto[] partsDTO = (imp_part_dto[])serializer.Deserialize(new StringReader(inputXml));

            Part[] parts = partsDTO.Select(x => Mapper.Map<Part>(x))
                                   .Where(x => allowedSupplierIds.Contains(x.SupplierId))
                                   .ToArray();
            context.Parts.AddRange(parts);
            int addedParts = context.SaveChanges();
            return string.Format(importedMessage, addedParts);
        }

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            int[] allowedPartIds = context.Parts.Select(x => x.Id).ToArray();
            var serializer = new XmlSerializer(typeof(imp_car_dto[]), new XmlRootAttribute("Cars"));

            imp_car_dto[] carsDTO = (imp_car_dto[])serializer.Deserialize(new StringReader(inputXml));

            Car[] cars = carsDTO.Select(x => new Car()
            {
                Make = x.Make,
                Model = x.Model,
                TravelledDistance = x.TravelledDistance,
                PartCars = x.PartIds.Select(p => p.Id)
                .Distinct()
                .Where(p => allowedPartIds.Contains(p))
                .Select(p => new PartCar()
                {
                    PartId = p
                }).ToArray()
            }).ToArray();
            context.Cars.AddRange(cars);

            int addedCars = cars.Count();
            context.SaveChanges();
            return string.Format(importedMessage, addedCars);
        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(imp_customer_dto[]), new XmlRootAttribute("Customers"));

            imp_customer_dto[] customersDTO = (imp_customer_dto[])serializer.Deserialize(new StringReader(inputXml));

            Customer[] customers = customersDTO.Select(x => Mapper.Map<Customer>(x)).ToArray();
            context.Customers.AddRange(customers);

            int addedCustomers = context.SaveChanges();
            return string.Format(importedMessage, addedCustomers);
        }

        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            int[] allowedCarIds = context.Cars.Select(x => x.Id).ToArray();
            int[] allowedCustomerIds = context.Customers.Select(x => x.Id).ToArray();
            var serializer = new XmlSerializer(typeof(imp_sale_dto[]), new XmlRootAttribute("Sales"));

            imp_sale_dto[] salesDTO = (imp_sale_dto[])serializer.Deserialize(new StringReader(inputXml));

            Sale[] sales = salesDTO
                .Where(x => allowedCarIds.Contains(x.CarId) && allowedCustomerIds.Contains(x.CustomerId))
                .Select(x => Mapper.Map<Sale>(x)).ToArray();
            context.Sales.AddRange(sales);

            int addedCustomers = context.SaveChanges();
            return string.Format(importedMessage, addedCustomers);
        }

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            exp_carsWithDistance_dto[] cars = context.Cars.Where(x => x.TravelledDistance > 2000000)
                                     .OrderBy(x => x.Make)
                                     .ThenBy(x => x.Model)
                                     .Take(10)
                                     .ProjectTo<exp_carsWithDistance_dto>()
                                     .ToArray();

            var serializer = new XmlSerializer(typeof(exp_carsWithDistance_dto[]), new XmlRootAttribute("cars"));

            StringBuilder sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");
                serializer.Serialize(stringWriter, cars, namespaces);
            }
            return sb.ToString().Trim();
        }

        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            exp_BMWcar_dto[] cars = context.Cars.Where(x => x.Make == "BMW")
                                     .OrderBy(x => x.Model)
                                     .ThenByDescending(x => x.TravelledDistance)
                                     .ProjectTo<exp_BMWcar_dto>()
                                     .ToArray();


            var serializer = new XmlSerializer(typeof(exp_BMWcar_dto[]), new XmlRootAttribute("cars"));

            StringBuilder sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");
                serializer.Serialize(stringWriter, cars, namespaces);
            }
            return sb.ToString().Trim();
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var localSuppliers = context.Suppliers.Where(x => !x.IsImporter).Select(x => new exp_localSupplier_dto()
            {
                Id = x.Id,
                Name = x.Name,
                PartsCount = x.Parts.Count
            }).ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(exp_localSupplier_dto[]), new XmlRootAttribute("suppliers"));
            var sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                var xmlNamespaces = new XmlSerializerNamespaces();
                xmlNamespaces.Add("", "");
                serializer.Serialize(sw, localSuppliers, xmlNamespaces);
            }
            return sb.ToString().Trim();
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            exp_carWithParts_dto[] cars = context.Cars
                .Include(x => x.PartCars)
                .ThenInclude(pc => pc.Part)
                .OrderByDescending(x => x.TravelledDistance)
                .ThenBy(x => x.Model)
                .Take(5)
                .Select(x => new exp_carWithParts_dto()
                {
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance,
                    Parts = x.PartCars.OrderByDescending(p => p.Part.Price)
                                      .Select(p => new part_dto()
                                      {
                                          Name = p.Part.Name,
                                          Price = p.Part.Price
                                      }).ToArray()
                }).ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(exp_carWithParts_dto[]), new XmlRootAttribute("cars"));
            var sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                var xmlNamespaces = new XmlSerializerNamespaces();
                xmlNamespaces.Add("", "");
                serializer.Serialize(sw, cars, xmlNamespaces);
            }
            return sb.ToString().Trim();
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            exp_customerTotalPurchases_dto[] customers = context.Customers
                .Include(x => x.Sales)
                .ThenInclude(s => s.Car)
                .ThenInclude(c => c.PartCars)
                .ThenInclude(pc => pc.Part)
                .Where(x => x.Sales.Any())
                .Select(x => new exp_customerTotalPurchases_dto()
                {
                    Name = x.Name,
                    BoughtCars = x.Sales.Count,
                    SpentMoney = x.Sales.SelectMany(z => z.Car.PartCars).Select(p => p.Part.Price).Sum()
                })
                .OrderByDescending(x => x.SpentMoney)
                .ToArray();



            XmlSerializer serializer = new XmlSerializer(typeof(exp_customerTotalPurchases_dto[]), new XmlRootAttribute("customers"));
            var sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                var xmlNamespaces = new XmlSerializerNamespaces();
                xmlNamespaces.Add("", "");
                serializer.Serialize(sw, customers, xmlNamespaces);
            }
            return sb.ToString().Trim();
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            exp_SalesWithDiscount_dto[] sales = context.Sales
                                                     .Include(x => x.Customer)
                                                     .Include(x => x.Car)
                                                     .ThenInclude(c => c.PartCars)
                                                     .ThenInclude(pc => pc.Part)
                                                     .Where(x => x.Discount > 0)
                                                     .Select(x => new exp_SalesWithDiscount_dto()
                                                     {
                                                         Car = new car_dto1()
                                                         {
                                                             Make = x.Car.Make,
                                                             Model = x.Car.Model,
                                                             TravelledDistance = x.Car.TravelledDistance
                                                         },
                                                         CustomerName=x.Customer.Name,
                                                         Discount = x.Discount,
                                                         Price = x.Car.PartCars.Select(pc => pc.Part.Price).Sum(),
                                                         PriceDiscounted = x.Car.PartCars.Select(pc => pc.Part.Price).Sum() - x.Discount,

                                                     })
                                                     .ToArray();



            XmlSerializer serializer = new XmlSerializer(typeof(exp_SalesWithDiscount_dto[]), new XmlRootAttribute("sales"));
            var sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                var xmlNamespaces = new XmlSerializerNamespaces();
                xmlNamespaces.Add("", "");
                serializer.Serialize(sw, sales, xmlNamespaces);
            }
            return sb.ToString().Trim();




        }



        //<sales>
        //  <sale>
        //    <car make = "BMW" model="M5 F10" travelled-distance="435603343" />
        //    <discount>30.00</discount>
        //    <customer-name>Hipolito Lamoreaux</customer-name>
        //    <price>707.97</price>
        //    <price-with-discount>495.58</price-with-discount>
        //  </ExportSaleDiscount>




        //   private static void AddDiscount(CarDealerContext context,Sale[] newSales)
        //  {
        //      int[] newIds = newSales.Select(x => x.Id).ToArray();
        //      var sales = context.Sales.Include(x => x.Customer).Where(x => newIds.Contains(x.Id) && x.Customer.IsYoungDriver).ToArray();
        //      foreach (var sale in sales)
        //      {
        //          sale.Discount += 5;
        //      }
        //  }
    }
}