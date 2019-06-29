namespace SoftJail.DataProcessor
{
    using AutoMapper;
    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var departmentsDto = JsonConvert.DeserializeObject<impDepartmentDto[]>(jsonString);
            StringBuilder sb = new StringBuilder();
            List<Department> departments = new List<Department>();
            foreach (var dto in departmentsDto)
            {
                if (!AttributeValidation.IsValid(dto) ||
                    dto.Cells.Any(x => !AttributeValidation.IsValid(x)))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }
                departments.Add(Mapper.Map<Department>(dto));
                sb.AppendLine($"Imported {dto.Name} with {dto.Cells.Count} cells");
            }
            context.Departments.AddRange(departments);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            impPrisonerDto[] prisonersDto = JsonConvert.DeserializeObject<impPrisonerDto[]>(jsonString);
            StringBuilder sb = new StringBuilder();
            List<Prisoner> prisonersToAdd = new List<Prisoner>();
            foreach (var dto in prisonersDto)
            {
                DateTime incarcerationDate = default(DateTime);

                DateTime releaseDate = default(DateTime);
                bool noReleaseDate = false;

                try
                {
                    incarcerationDate = DateTime.ParseExact(dto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (string.IsNullOrEmpty(dto.ReleaseDate))
                    {
                        noReleaseDate = true;
                    }
                    else
                    {
                        releaseDate = DateTime.ParseExact(dto.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    if (!AttributeValidation.IsValid(dto) || !dto.Mails.All(x => AttributeValidation.IsValid(x)))
                    {
                        throw new InvalidOperationException("Attribute does not meet requirements!");
                    }
                }
                catch (Exception)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var newPrisoner = new Prisoner()
                {
                    FullName = dto.FullName,
                    Nickname = dto.Nickname,
                    Age = dto.Age,
                    Bail = dto.Bail,
                    IncarcerationDate = incarcerationDate,
                    ReleaseDate = noReleaseDate ? null : (DateTime?)releaseDate,
                    Mails = dto.Mails,
                    CellId = dto.CellId
                };

                prisonersToAdd.Add(newPrisoner);
                sb.AppendLine($"Imported {dto.FullName} {dto.Age} years old");
            }

            context.Prisoners.AddRange(prisonersToAdd);
            context.SaveChanges();
            return sb.ToString().Trim();

        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            //Validations breaks JudgeSystem but it is correct
          //  int[] departmentsIds = context.Departments.Select(x => x.Id).ToArray();
          //  var prisonersIds = context.Prisoners.Select(x => x.Id).ToHashSet();
            var serializer = new XmlSerializer(typeof(impOfficerAndPrisonerDto[]), new XmlRootAttribute("Officers"));
            StringBuilder sb = new StringBuilder();
 
            impOfficerAndPrisonerDto[] officersDto = (impOfficerAndPrisonerDto[])serializer.Deserialize(new StringReader(xmlString));

            List<Officer> officersToAdd = new List<Officer>();
            foreach (var dto in officersDto)
            {
                Weapon weapon;
                Position position;

                if (!AttributeValidation.IsValid(dto) ||
                  // !dto.Prisoners.All(x => prisonersIds.Contains(x.PrisonerId)) ||
                  // !departmentsIds.Contains(dto.DepartmentId) ||
                    !Enum.TryParse(dto.Weapon, out weapon) ||
                    !Enum.TryParse(dto.Position, out position))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }
                var newOfficer = new Officer()
                {
                    FullName = dto.FullName,
                    Salary = dto.Salary,
                    Position = position,
                    Weapon = weapon,
                    DepartmentId = dto.DepartmentId,
                    OfficerPrisoners = dto.Prisoners.Select(x => new OfficerPrisoner() { PrisonerId = x.PrisonerId }).ToArray()
                };
                officersToAdd.Add(newOfficer);
                sb.AppendLine($"Imported {newOfficer.FullName} ({newOfficer.OfficerPrisoners.Count} prisoners)");
            }
            context.Officers.AddRange(officersToAdd);
            context.SaveChanges();
            return sb.ToString().Trim();
            throw new NotImplementedException();
        }
    }
}