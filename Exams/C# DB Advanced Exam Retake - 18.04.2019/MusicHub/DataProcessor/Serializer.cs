namespace MusicHub.DataProcessor
{
    using Data;
    using MusicHub.DataProcessor.ExportDtos;
    using Newtonsoft.Json;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albumsOfProducer = context.Albums
                                          .Where(x => x.ProducerId == producerId)
                                          .OrderByDescending(a => a.Songs.Sum(s => s.Price))
                                          .Select(x => new
                                          {
                                              AlbumName = x.Name,
                                              ReleaseDate = x.ReleaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                                              ProducerName = x.Producer.Name,
                                              Songs = x.Songs.Select(s => new
                                              {
                                                  SongName = s.Name,
                                                  Price = s.Price.ToString("F2"),
                                                  Writer = s.Writer.Name
                                              }).OrderByDescending(s => s.SongName).ThenBy(s => s.Writer).ToArray(),
                                              AlbumPrice = x.Songs.Sum(s => s.Price).ToString("F2")
                                          }).ToArray();
            string resultJson = JsonConvert.SerializeObject(albumsOfProducer, Formatting.Indented);
            return resultJson;
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var sb = new StringBuilder();
            var longSongs = context.Songs.Where(x => x.Duration.TotalSeconds > duration)
               .Select(s => new expXmlSong()
               {
                   SongName = s.Name,
                   Writer = s.Writer.Name,
                   AlbumProducer = s.Album.Producer.Name,
                   PerformerFullName = s.SongPerformers.Any()
               ? s.SongPerformers.Select(p => $"{p.Performer.FirstName} {p.Performer.LastName}").First()
               : null,
                   Duration = s.Duration.ToString("c", CultureInfo.InvariantCulture)
               }).OrderBy(s => s.SongName)
                 .ThenBy(s => s.Writer)
                 .ThenBy(s => s.PerformerFullName)
                 .ToArray();

            var serializer = new XmlSerializer(typeof(expXmlSong[]), new XmlRootAttribute("Songs"));

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            using (var sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, longSongs, ns);
            }
            return sb.ToString().Trim();
        }
    }
}