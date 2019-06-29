using AutoMapper;
using Cinema.Data.Models;
using Cinema.Data.Models.Enums;
using Cinema.DataProcessor.ImportDto;
using System;
using System.Globalization;
using System.Linq;

namespace Cinema
{
    public class CinemaProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public CinemaProfile()
        {

            CreateMap<moviesJsonDto, Movie>()
                .ForMember(d => d.Genre, opt => opt.MapFrom(s => Enum.Parse<Genre>(s.Genre)))
                .ForMember(d => d.Duration, opt => opt.MapFrom(s => TimeSpan.ParseExact(s.Duration, @"hh\:mm\:ss", null, TimeSpanStyles.None)));
            CreateMap<hallJsonDto, Hall>();

            CreateMap<projectionXmlDto, Projection>()
                .ForMember(d => d.DateTime, opt => opt.MapFrom(s => DateTime.ParseExact(s.DateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)));

            CreateMap<cutomerXmlDto, Customer>()
                .ForMember(d=>d.Tickets,opt=>opt.MapFrom(s=>s.Tickets.Select(x=>Mapper.Map<Ticket>(x))));
            CreateMap<ticketXmlDto, Ticket>();

        }
    }
}