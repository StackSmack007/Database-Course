namespace FastFood.DataProcessor
{
    using FastFood.Data;
    using FastFood.DataProcessor.Dto.Export;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
        {
            var orders = context.Orders
                .Where(x => x.Employee.Name == employeeName && x.Type.ToString() == orderType)
                .Include(x => x.OrderItems).ThenInclude(oi => oi.Item)
                .ToArray();
            decimal TotalMoneyMade = orders.SelectMany(x => x.OrderItems).Select(x => x.Quantity * x.Item.Price).Sum();
            var orderDTOs = new
            {
                Name = employeeName,
                Orders = orders.Select(o => new
                {
                    o.Customer,
                    Items = o.OrderItems.Select(oi => new
                    {
                        oi.Item.Name,
                        oi.Item.Price,
                        oi.Quantity
                    }).ToArray(),
                    TotalPrice = o.OrderItems.Select(x => x.Quantity * x.Item.Price).Sum()
                }).OrderByDescending(x => x.TotalPrice)
                       .ThenByDescending(x => x.Items.Count())
                       .ToArray(),

                TotalMade = TotalMoneyMade
            };
            string result = JsonConvert.SerializeObject(orderDTOs, Formatting.Indented);
            return result;
        }

        public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
        {
            string[] categoriesOfInterest = categoriesString.Split(',');
            var categoriesDtos = context.Categories.Where(x => categoriesOfInterest.Contains(x.Name))
               .Select(x => new
               {
                   x.Name,
                   MostPopularItem = x.Items.OrderByDescending(i => i.Price * i.OrderItems.Sum(oi => oi.Quantity))
                                            .First()
               })
                .Select(x => new dto_categoryEXP_xml()
                {
                    Name = x.Name,
                    MostPopularItem = new dto_itemEXP_xml()
                    {
                        Name = x.MostPopularItem.Name,
                        TotalMade = x.MostPopularItem.Price * x.MostPopularItem.OrderItems.Sum(oi => oi.Quantity),
                        TimesSold = x.MostPopularItem.OrderItems.Sum(oi => oi.Quantity)
                    }
                }).OrderByDescending(x => x.MostPopularItem.TotalMade)
                .ThenByDescending(x => x.MostPopularItem.TimesSold)
                .ToArray();

            var serializer = new XmlSerializer(typeof(dto_categoryEXP_xml[]), new XmlRootAttribute("Categories"));

            StringBuilder sb = new StringBuilder();

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            using (StringWriter sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, categoriesDtos, ns);
            }
            return sb.ToString().Trim();
        }
    }
}