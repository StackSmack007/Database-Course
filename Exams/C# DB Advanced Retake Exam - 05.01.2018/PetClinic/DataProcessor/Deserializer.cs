namespace PetClinic.DataProcessor
{
    using AutoMapper;
    using Newtonsoft.Json;
    using PetClinic.Data;
    using PetClinic.DataProcessor.DTOS.Import;
    using PetClinic.Models;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        private const string invalidEntryMessage = "Error: Invalid data.";

        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            AnimalAid[] animalAids = JsonConvert.DeserializeObject<AnimalAid[]>(jsonString);
            StringBuilder sb = new StringBuilder();

            List<AnimalAid> animalAidsToBeAdded = new List<AnimalAid>();
            foreach (var animalAid in animalAids)
            {
                if (AttributeValidator.IsValid(animalAid) && animalAidsToBeAdded.All(x => x.Name != animalAid.Name)
                   )
                {
                    animalAidsToBeAdded.Add(animalAid);
                    sb.AppendLine($"Record {animalAid.Name} successfully imported.");
                    continue;
                }
                sb.AppendLine(invalidEntryMessage);
            }
            context.AnimalAids.AddRange(animalAidsToBeAdded);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            var animalsDTOs = JsonConvert.DeserializeObject<imp_json_animalDto[]>(jsonString);
            StringBuilder sb = new StringBuilder();
            List<Animal> animalsToBeAdded = new List<Animal>();
            List<string> availablePassportNumbers = context.Passports.Select(x => x.SerialNumber).ToList();
            foreach (var dto in animalsDTOs)
            {
                DateTime registrationDate;

                if (DateTime.TryParseExact(dto.Passport.RegistrationDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out registrationDate) &&
                    AttributeValidator.IsValid(dto) && AttributeValidator.IsValid(dto.Passport) && !availablePassportNumbers.Contains(dto.Passport.SerialNumber))
                {
                    var newAnimal = new Animal()
                    {
                        Name = dto.Name,
                        Age = dto.Age,
                        Type = dto.Type,
                        Passport = new Passport()
                        {
                            SerialNumber = dto.Passport.SerialNumber,
                            OwnerName = dto.Passport.OwnerName,
                            OwnerPhoneNumber = dto.Passport.OwnerPhoneNumber,
                            RegistrationDate = registrationDate
                        }
                    };
                    animalsToBeAdded.Add(newAnimal);
                    availablePassportNumbers.Add(newAnimal.Passport.SerialNumber);
                    sb.AppendLine($"Record {newAnimal.Name} Passport №: {newAnimal.Passport.SerialNumber} successfully imported.");
                    continue;
                }
                sb.AppendLine(invalidEntryMessage);
            }
            context.Animals.AddRange(animalsToBeAdded);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportVets(PetClinicContext context, string xmlString)
        {
            List<string> phoneNumbersAvailable = context.Vets.Select(x => x.PhoneNumber).ToList();

            var serializer = new XmlSerializer(typeof(imp_xml_vetDto[]), new XmlRootAttribute("Vets"));

            var vetsDtos = (imp_xml_vetDto[])serializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();

            List<Vet> vetsToBeAdded = new List<Vet>();

            foreach (var dto in vetsDtos)
            {
                if (AttributeValidator.IsValid(dto) && phoneNumbersAvailable.All(x => x != dto.PhoneNumber))
                {
                    phoneNumbersAvailable.Add(dto.PhoneNumber);
                    Vet newVet = Mapper.Map<Vet>(dto);
                    vetsToBeAdded.Add(newVet);
                    sb.AppendLine($"Record {newVet.Name} successfully imported.");
                    continue;
                }
                sb.AppendLine(invalidEntryMessage);
            }

            context.Vets.AddRange(vetsToBeAdded);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            var vet_Name_Id = context.Vets.GroupBy(x => x.Name).Select(x => x.Last()).ToDictionary(x => x.Name, x => x.Id);
            var animal_PassportNumber_Id = context.Animals.Select(x => new { x.Passport.SerialNumber, x.Id }).ToDictionary(x => x.SerialNumber, x => x.Id);
            var animalAid_Name_Id = context.AnimalAids.ToDictionary(x => x.Name, x => x.Id);

            var serializer = new XmlSerializer(typeof(imp_xml_procedureDto[]), new XmlRootAttribute("Procedures"));
            StringBuilder sb = new StringBuilder();

            var proceduresDtos = (imp_xml_procedureDto[])serializer.Deserialize(new StringReader(xmlString));
            List<Procedure> proceduresToBeAdded = new List<Procedure>();
            Dictionary<string, DateTime> doctorTimes = new Dictionary<string, DateTime>();

            foreach (var dto in proceduresDtos)
            {
                DateTime dateTime;

                bool vetNotFound = !vet_Name_Id.ContainsKey(dto.VetName);
                bool animalNotFound = !animal_PassportNumber_Id.ContainsKey(dto.AnimalPassportNumber);
                bool animalAidNotFound = !dto.AnimalAids.All(x => animalAid_Name_Id.ContainsKey(x.Name));

                bool animalAidRepeats = false;
                for (int i = 0; i < dto.AnimalAids.Count; i++)
                {
                    bool endCycle = false;
                    for (int j = 0; j < dto.AnimalAids.Count; j++)
                    {
                        if (dto.AnimalAids[i].Name == dto.AnimalAids[j].Name && i != j)
                        {
                            animalAidRepeats = true;
                            endCycle = true;
                            break;
                        }
                    }
                    if (endCycle) break;
                }

                if (vetNotFound || animalNotFound || animalAidNotFound || !dto.AnimalAids.Any() || animalAidRepeats ||
                 !DateTime.TryParseExact(dto.DateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                {
                    sb.AppendLine(invalidEntryMessage);
                    continue;
                }

                Procedure newProcedure = new Procedure()
                {
                    VetId = vet_Name_Id[dto.VetName],
                    AnimalId = animal_PassportNumber_Id[dto.AnimalPassportNumber],
                    DateTime = dateTime,
                    ProcedureAnimalAids = dto.AnimalAids.Select(x => new ProcedureAnimalAid()
                    {
                        AnimalAidId = animalAid_Name_Id[x.Name]
                    }).ToArray()
                };

                if (proceduresToBeAdded.Any(x => x.DateTime == newProcedure.DateTime &&
                                                 x.VetId == newProcedure.VetId &&
                                                 x.AnimalId == newProcedure.AnimalId))
                {
                    sb.AppendLine(invalidEntryMessage);
                    continue;
                }

                proceduresToBeAdded.Add(newProcedure);
                sb.AppendLine("Record successfully imported.");
            }

            context.Procedures.AddRange(proceduresToBeAdded);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        private class NameComparator : IEqualityComparer<NameIdPair>
        {
            public bool Equals(NameIdPair x, NameIdPair y)
            {
                return x.Name == y.Name;
            }

            public int GetHashCode(NameIdPair obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}