namespace Cinema.DataProcessor
{
    using Cinema.DataProcessor.ExportDto;
    using Data;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            var topMovies = context.Movies
                       .Where(x => x.Rating >= rating && x.Projections.Any(p => p.Tickets.Any()))
                       .OrderByDescending(x => x.Rating)
                       .ThenByDescending(x => x.Projections.SelectMany(p => p.Tickets).Sum(p => p.Price))
                                                         .Select(x => new
                                                         {
                                                             MovieName = x.Title,
                                                             Rating = x.Rating.ToString("F2"),
                                                             TotalIncomes = x.Projections.SelectMany(p => p.Tickets).Sum(p => p.Price).ToString("F2"),
                                                             Customers = x.Projections.SelectMany(p => p.Tickets)
                           .Select(c => new
                           {
                               c.Customer.FirstName,
                               c.Customer.LastName,
                               Balance = $"{c.Customer.Balance:F2}",
                           })
                           .OrderByDescending(c => c.Balance)
                           .ThenBy(c => c.FirstName)
                           .ThenBy(c => c.LastName)
                           .ToArray()
                                                         })
                       .Take(10)
                       .ToArray();

            string jsonResult = JsonConvert.SerializeObject(topMovies, Formatting.Indented);
            return jsonResult;
        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            var result = context.Customers.Where(x => x.Age >= age).OrderByDescending(x => x.Tickets.Sum(t => t.Price))
                .Select(x => new expCustomerDtoXML()
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                SpentMoney = x.Tickets.Sum(t => t.Price).ToString("F2"),
                SpentTime = new TimeSpan((x.Tickets.Select(t => t.Projection).Select(p => p.Movie.Duration).Sum(d => d.Ticks))).ToString(@"hh\:mm\:ss")
            })
            .Take(10).ToArray();

            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(expCustomerDtoXML[]), new XmlRootAttribute("Customers"));

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            using (var sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, result, ns);
            }

            return sb.ToString().Trim();
        }
    }
}