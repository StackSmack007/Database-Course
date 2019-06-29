namespace MusicHub.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using Data;
    using MusicHub.Data.Models;
    using MusicHub.DataProcessor.ImportDtos;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private static StringBuilder sb = new StringBuilder();

        private const string ErrorMessage = "Invalid data";

        private const string SuccessfullyImportedWriter
            = "Imported {0}";
        private const string SuccessfullyImportedProducerWithPhone
            = "Imported {0} with phone: {1} produces {2} albums";
        private const string SuccessfullyImportedProducerWithNoPhone
            = "Imported {0} with no phone number produces {1} albums";
        private const string SuccessfullyImportedSong
            = "Imported {0} ({1} genre) with duration {2}";
        private const string SuccessfullyImportedPerformer
            = "Imported {0} ({1} songs)";

        public static string ImportWriters(MusicHubDbContext context, string jsonString)
        {
            Writer[] writersImported = JsonConvert.DeserializeObject<Writer[]>(jsonString);
            sb.Clear();
            var writersToBeAdded = new Queue<Writer>();
            foreach (var writer in writersImported)
            {
                if (MassAttributeValidator.IsValid(writer))
                {
                    writersToBeAdded.Enqueue(writer);
                    sb.AppendLine(string.Format(SuccessfullyImportedWriter, writer.Name));
                    continue;
                }
                sb.AppendLine(ErrorMessage);
            }
            context.Writers.AddRange(writersToBeAdded);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportProducersAlbums(MusicHubDbContext context, string jsonString)
        {
            sb.Clear();
            var producersDto = JsonConvert.DeserializeObject<impJsonDtoProducer[]>(jsonString);

            var producersToBeAdded = new Queue<Producer>();

            foreach (var dto in producersDto)
            {
                try
                {
                    var newProducer = Mapper.Map<Producer>(dto);
                    if (MassAttributeValidator.IsValid(newProducer) && newProducer.Albums.All(x => MassAttributeValidator.IsValid(x)))
                    {
                        producersToBeAdded.Enqueue(newProducer);
                        if (string.IsNullOrEmpty(newProducer.PhoneNumber))
                        {
                            sb.AppendLine(string.Format(SuccessfullyImportedProducerWithNoPhone, newProducer.Name, newProducer.Albums.Count));
                        }
                        else
                        {
                            sb.AppendLine(string.Format(SuccessfullyImportedProducerWithPhone, newProducer.Name, newProducer.PhoneNumber, newProducer.Albums.Count));
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("incorrect data input MassValidator detected anomaly");
                    }

                }
                catch (Exception)
                {
                    sb.AppendLine(ErrorMessage);
                }
            }
            context.Producers.AddRange(producersToBeAdded);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportSongs(MusicHubDbContext context, string xmlString)
        {
            sb.Clear();
            var serializer = new XmlSerializer(typeof(impXmlDtoSong[]), new XmlRootAttribute("Songs"));

            int[] albumIdsAvailable = context.Albums.Select(x => x.Id).ToArray();
            int[] writersIdsAvailable = context.Writers.Select(x => x.Id).ToArray();

            var songsDtos = (impXmlDtoSong[])serializer.Deserialize(new StringReader(xmlString));
            var songsToBeAdded = new Queue<Song>();
            foreach (var dto in songsDtos)
            {
                try
                {
                    Song newSong = Mapper.Map<Song>(dto);
                    if (MassAttributeValidator.IsValid(newSong))
                    {
                        if (dto.AlbumId != null && !albumIdsAvailable.Contains(dto.AlbumId.Value))
                        {
                            throw new InvalidOperationException($"Album with Id {dto.AlbumId.Value} does not exist in database!");
                        }
                        if (!writersIdsAvailable.Contains(dto.WriterId))
                        {
                            throw new InvalidOperationException($"Writer with Id {dto.WriterId} does not exist in database!");
                        }

                        sb.AppendLine(string.Format(SuccessfullyImportedSong, dto.Name, dto.Genre, dto.Duration));
                        songsToBeAdded.Enqueue(newSong);
                        continue;
                    }
                    throw new InvalidOperationException("incorrect data input MassValidator detected anomaly");

                }
                catch (Exception)
                {
                    sb.AppendLine(ErrorMessage);
                }
            }
            context.Songs.AddRange(songsToBeAdded);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportSongPerformers(MusicHubDbContext context, string xmlString)
        {
            sb.Clear();
            var serializer = new XmlSerializer(typeof(impXmlDtoPerformer[]), new XmlRootAttribute("Performers"));

            int[] songIdsAvailable = context.Songs.Select(x => x.Id).ToArray();

            var performerDtos = (impXmlDtoPerformer[])serializer.Deserialize(new StringReader(xmlString));
            var performersToBeAdded = new Queue<Performer>();
            foreach (var dto in performerDtos)
            {
                try
                {
                    var newPerformer = new Performer()
                    {
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Age = dto.Age,
                        NetWorth = dto.NetWorth,
                        PerformerSongs = dto.SongIds.Select(x => new SongPerformer()
                        {
                            SongId = x.SongId
                        }).ToHashSet()
                    };

                    if (MassAttributeValidator.IsValid(newPerformer))
                    {
                        if (newPerformer.PerformerSongs.Any(x => !songIdsAvailable.Contains(x.SongId)))
                        {
                            throw new InvalidOperationException($"This Performer can sing unknown songs");
                        }
                        if (dto.SongIds.Select(x => x.SongId).Distinct().Count() < dto.SongIds.Count())
                        {
                            throw new InvalidOperationException($"This Performer sings 1 song more than once!");
                        }

                        sb.AppendLine(string.Format(SuccessfullyImportedPerformer, dto.FirstName, dto.SongIds.Count));
                        performersToBeAdded.Enqueue(newPerformer);
                        continue;
                    }
                    throw new InvalidOperationException("incorrect data input MassValidator detected anomaly");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    sb.AppendLine(ErrorMessage);
                }
            }
            context.Performers.AddRange(performersToBeAdded);
            context.SaveChanges();
            return sb.ToString().Trim();
        }
    }
}