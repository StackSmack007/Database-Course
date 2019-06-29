namespace SoftJail
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using SoftJail.Data.Models;
    using SoftJail.DataProcessor.ImportDto;
    using System.Linq;

    public class SoftJailProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public SoftJailProfile()
        {
            CreateMap<impDepartmentDto, Department>()
                .ForMember(d => d.Cells, opt => opt.MapFrom(s => s.Cells.AsQueryable().ProjectTo<Cell>()));
            CreateMap<impCellDto, Cell>();

     

        }
    }
}
