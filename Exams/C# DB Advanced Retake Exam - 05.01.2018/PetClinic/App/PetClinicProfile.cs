namespace PetClinic.App
{
    using AutoMapper;
    using PetClinic.DataProcessor.DTOS.Import;
    using PetClinic.Models;

    public class PetClinicProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public PetClinicProfile()
        {
            CreateMap<imp_xml_vetDto, Vet>();
        }
    }
}