using System;
using System.Linq;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Newtonsoft.Json;

using PetClinic.App;
using PetClinic.Models;
using PetClinic.Data;

[TestFixture]
public class Import_000_019
{
    private IServiceProvider serviceProvider;

    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();

        services
            .AddDbContext<PetClinicContext>(
                options => options.UseInMemoryDatabase("PetClinic")
            );

        Mapper.Reset();
        Mapper.Initialize(cfg => cfg.AddProfile<PetClinicProfile>());

        this.serviceProvider = services.BuildServiceProvider();
    }

    [Test]
    public void ImportProceduresZeroTest4()
    {
        var context = serviceProvider.GetService<PetClinicContext>();

        SeedDatabase(context);

        var inputXml = @"<Procedures><Procedure><Vet>Michael Jordan</Vet><Animal>etyhGgH678</Animal><AnimalAids><AnimalAid><Name>Fecal Test</Name></AnimalAid><AnimalAid><Name>Internal Deworming</Name></AnimalAid></AnimalAids><DateTime>12-05-1995</DateTime></Procedure><Procedure><Vet>Michael Jordan</Vet><Animal>etyhGgH678</Animal><AnimalAids><AnimalAid><Name>Fecal Test</Name></AnimalAid><AnimalAid><Name>Internal Deworming</Name></AnimalAid><AnimalAid><Name>Fecal Test</Name></AnimalAid></AnimalAids><DateTime>12-05-1995</DateTime></Procedure></Procedures>";

        var actualOutput = PetClinic.DataProcessor.Deserializer.ImportProcedures(context, inputXml).TrimEnd();
        var expectedOutput = "Record successfully imported.\r\nError: Invalid data.";

        var assertContext = serviceProvider.GetService<PetClinicContext>();

        var expectedProceduresCount = 1;
        var actualProceduresCount = assertContext.Procedures.Count();

        Assert.That(actualProceduresCount, Is.EqualTo(expectedProceduresCount), "Number of inserted Procedures is incorrect!");

        Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip, "ImportProcedures output is incorrect!");
    }

    private static void SeedDatabase(PetClinicContext context)
    {
        string animalsJson = "[{'Name':'Bella','Passport':{'SerialNumber':'etyhGgH678'}}]";
        string vetsJson = "[{'Name':'Michael Jordan','PhoneNumber':'0897665544'}]";
        string animalAidsJson = "[{'Name':'Internal Deworming'},{'Name':'Fecal Test'},{'Name':'H3N8'},{'Name':'Nasal Bordetella'},{'Name':'Bordetella'},{'Name':'Lepto 4'},{'Name':'Injectable Bordetella'},{'Name':'H3N2'},{'Name':'Canine Rabbies Vaccine'},{'Name':'Canine Heartworm Test'},{'Name':'Lyme Test'},{'Name':'PureVax Rabies'}]";

        var animals = JsonConvert.DeserializeObject<Animal[]>(animalsJson);
        var vets = JsonConvert.DeserializeObject<Vet[]>(vetsJson);
        var animalAids = JsonConvert.DeserializeObject<AnimalAid[]>(animalAidsJson);

        context.Animals.AddRange(animals);
        context.Vets.AddRange(vets);
        context.AnimalAids.AddRange(animalAids);

        context.SaveChanges();
    }
}