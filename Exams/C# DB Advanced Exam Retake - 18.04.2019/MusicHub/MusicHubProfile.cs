namespace MusicHub
{
    using AutoMapper;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.DataProcessor.ImportDtos;
    using System;
    using System.Globalization;
    using System.Linq;

    public class MusicHubProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public MusicHubProfile()
        {

            CreateMap<impJsonDtoProducer, Producer>()
                            .ForMember(d => d.Albums, opt => opt.MapFrom(s => s.Albums.Select(x => Mapper.Map<impJsonDtoAlbum, Album>(x))));

            CreateMap<impJsonDtoAlbum, Album>()
                           .ForMember(d => d.ReleaseDate, opt => opt
                           .MapFrom(s => DateTime.ParseExact(s.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)));


            CreateMap<impXmlDtoSong, Song>()
                .ForMember(d => d.Duration, opt => opt.MapFrom(s => TimeSpan.ParseExact(s.Duration, @"hh\:mm\:ss", null, TimeSpanStyles.None)))
                .ForMember(d => d.CreatedOn, opt => opt.MapFrom(s => DateTime.ParseExact(s.CreatedOn, @"dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(d => d.Genre, opt => opt.MapFrom(s => Enum.Parse<Genre>(s.Genre)));

        }
    }
}