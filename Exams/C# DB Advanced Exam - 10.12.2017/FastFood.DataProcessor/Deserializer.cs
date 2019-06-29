namespace FastFood.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using FastFood.Data;
    using FastFood.DataProcessor.Dto.Import;
    using FastFood.Models;
    using FastFood.Models.Enums;
    using Newtonsoft.Json;

    public static class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessMessage = "Record {0} successfully imported.";

        public static string ImportEmployees(FastFoodDbContext context, string jsonString)
        {
            dto_employee_Json[] resultsDTOs = JsonConvert.DeserializeObject<dto_employee_Json[]>(jsonString);

            StringBuilder sb = new StringBuilder();
            List<Employee> empsToBeAdded = new List<Employee>();
            foreach (var dto in resultsDTOs)
            {
                Employee newEmp = new Employee();
                try
                {
                    newEmp.Age = int.Parse(dto.Age);
                    newEmp.Name = dto.Name;
                    newEmp.Position = context.Positions.FirstOrDefault(x => x.Name == dto.Position) ??
                        empsToBeAdded.Select(x => x.Position).FirstOrDefault(x => x.Name == dto.Position);

                    if (newEmp.Position is null)
                    {
                        newEmp.Position = new Position() { Name = dto.Position };
                    }

                    if (!AttributeValidator.IsValid(newEmp, newEmp.Position))
                    {
                        throw new ArgumentException("Invalid DataMember!");
                    }
                    empsToBeAdded.Add(newEmp);
                    sb.AppendLine(string.Format(SuccessMessage, newEmp.Name));
                }
                catch (Exception)
                {
                    sb.AppendLine(FailureMessage);
                }
            }
            context.Employees.AddRange(empsToBeAdded);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportItems(FastFoodDbContext context, string jsonString)
        {
            return "";
            dto_item_Json[] resultsDTOs = JsonConvert.DeserializeObject<dto_item_Json[]>(jsonString);

            StringBuilder sb = new StringBuilder();
            List<Item> itemsToBeAdded = new List<Item>();
            foreach (var dto in resultsDTOs)
            {
                var newItem = new Item();
                try
                {
                    newItem.Name = dto.Name;
                    newItem.Price = decimal.Parse(dto.Price);

                    newItem.Category = context.Categories.FirstOrDefault(x => x.Name == dto.Category) ??
                        itemsToBeAdded.Select(x => x.Category).FirstOrDefault(x => x.Name == dto.Category);

                    if (newItem.Category is null)
                    {
                        newItem.Category = new Category() { Name = dto.Category };
                    }
                    bool ItemNameFree = context.Items.All(x => x.Name != dto.Name) &&
                                         itemsToBeAdded.All(x => x.Name != dto.Name);

                    if (!ItemNameFree || !AttributeValidator.IsValid(newItem, newItem.Category))
                    {
                        throw new ArgumentException("Invalid DataMember!");
                    }
                    itemsToBeAdded.Add(newItem);
                    sb.AppendLine(string.Format(SuccessMessage, newItem.Name));
                }
                catch (Exception)
                {
                    sb.AppendLine(FailureMessage);
                }
            }
            context.Items.AddRange(itemsToBeAdded);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportOrders(FastFoodDbContext context, string xmlString)
        {

            var serializer = new XmlSerializer(typeof(dto_order_Xml[]), new XmlRootAttribute("Orders"));

            var dtos = (dto_order_Xml[])serializer.Deserialize(new StringReader(xmlString));
            StringBuilder sb = new StringBuilder();
            List<Order> ordersToBeAdded = new List<Order>();

            Item[] menuItems = context.Items.ToArray();
            foreach (var dto in dtos)
            {
                var newOrder = new Order();
                try
                {
                    newOrder.DateTime = DateTime.ParseExact(dto.DateTime, "dd/MM/yyyy HH:mm",CultureInfo.InvariantCulture);

                    newOrder.Customer = dto.CustomerName;

                    newOrder.Employee = context.Employees.FirstOrDefault(x => x.Name == dto.EmployeeName);
                    newOrder.Type = Enum.Parse<OrderType>(dto.Type);
                    foreach (var itemDto in dto.Items)
                    {
                      var  item = menuItems.FirstOrDefault(x => x.Name == itemDto.Name);
                        if (item is null)
                        {
                            throw new ArgumentException("Item not found!");
                        }
                        newOrder.OrderItems.Add(new OrderItem() { Quantity = int.Parse(itemDto.QuantityINT), Item = item });
                    }

                    if (newOrder.Employee is null ||
                        !AttributeValidator.IsValid(newOrder)||
                        !newOrder.OrderItems.All(x=>AttributeValidator.IsValid(x)))
                    {
                        throw new ArgumentException("Invalid DataMember!");
                    }
                    ordersToBeAdded.Add(newOrder);
                    sb.AppendLine(string.Format("Order for {0} on {1} added", dto.CustomerName,dto.DateTime));
                }
                catch (Exception)
                {
                    sb.AppendLine(FailureMessage);
                }
            }
            context.Orders.AddRange(ordersToBeAdded);
            context.SaveChanges();
            return sb.ToString().Trim();


        }
    }
}