namespace SoftJail.DataProcessor
{
    using Data;
    using Newtonsoft.Json;
    using SoftJail.DataProcessor.ExportDto;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisoners = context.Prisoners
                .Where(x => ids.Contains(x.Id))
                .OrderBy(x => x.FullName).ThenBy(x => x.Id)
                .Select(x => new
                {
                    x.Id,
                    Name = x.FullName,
                    x.Cell.CellNumber,
                    Officers = x.PrisonerOfficers.Select(op => new
                    {
                        OfficerName = op.Officer.FullName,
                        Department = op.Officer.Department.Name
                    }).OrderBy(o => o.OfficerName).ToArray(),
                    TotalOfficerSalary = x.PrisonerOfficers.Select(op => op.Officer.Salary).Sum()
                }).ToArray();
            string result = JsonConvert.SerializeObject(prisoners, Formatting.Indented);
            return result;
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            string[] namesOfPrisonersArray = prisonersNames.Split(",");

            var serializer = new XmlSerializer(typeof(xmlExp_inboxForPrisoner[]), new XmlRootAttribute("Prisoners"));

            var prisonersAndMailsDtos = context.Prisoners
                                             .Where(x => namesOfPrisonersArray.Contains(x.FullName))
                                             .OrderBy(x => x.FullName).ThenBy(x => x.Id)
                                             .Select(x => new xmlExp_inboxForPrisoner()
                                             {
                                                 Id = x.Id,
                                                 FullName = x.FullName,
                                                 IncarcerationDate = x.IncarcerationDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                                                 Mails = x.Mails.Select(m => new xmlMail()
                                                 {
                                                     Description = ReverseString(m.Description)
                                                 }).ToList()
                                             }).ToArray();
            StringBuilder sb = new StringBuilder();

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            using (StringWriter sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, prisonersAndMailsDtos, namespaces);
            }

            return sb.ToString().Trim();

        }

        private static string ReverseString(string message)
        {
            char[] charArray = message.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}