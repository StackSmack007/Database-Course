namespace PetClinic.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using PetClinic.Data;
    using PetClinic.DataProcessor.DTOS.Export;

    public class Serializer
    {
        public static string ExportAnimalsByOwnerPhoneNumber(PetClinicContext context, string phoneNumber)
        {
            var animalsOfOwner = context.Animals
                                      .Include(x => x.Passport)
                                      .Where(x => x.Passport.OwnerPhoneNumber == phoneNumber)
                                      .OrderBy(x => x.Age).ThenBy(x => x.Passport.SerialNumber)
                                      .Select(x => new
                                      {
                                          x.Passport.OwnerName,
                                          AnimalName = x.Name,
                                          x.Age,
                                          x.Passport.SerialNumber,
                                          RegisteredOn = x.Passport.RegistrationDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)
                                      }).ToArray();

            string result = JsonConvert.SerializeObject(animalsOfOwner, Formatting.Indented);
            return result;

        }

        public static string ExportAllProcedures(PetClinicContext context)
        {
            var proceduresDTOs = context.Procedures
                .OrderBy(x => x.DateTime).ThenBy(x => x.Animal.PassportSerialNumber)
                .Select(x => new exp_xml_procedureDto()
                {
                    PassportSerialNumber = x.Animal.PassportSerialNumber,
                    OwnerNumber = x.Animal.Passport.OwnerPhoneNumber,
                    DateTime = x.DateTime.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                    AnimalAids = x.ProcedureAnimalAids.Select(a => a.AnimalAid).Select(a => new exp_xml_animalAid()
                    {
                        Name = a.Name,
                        Price = a.Price
                    }).ToList(),
                  TotalPrice = x.ProcedureAnimalAids.Select(a => a.AnimalAid.Price).Sum()
                }).ToArray();

            var serializer = new XmlSerializer(typeof(exp_xml_procedureDto[]), new XmlRootAttribute("Procedures"));

            StringBuilder sb = new StringBuilder();

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");


            using (StringWriter sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, proceduresDTOs, ns);
            }

            return sb.ToString().Trim();
        }
    }
}
