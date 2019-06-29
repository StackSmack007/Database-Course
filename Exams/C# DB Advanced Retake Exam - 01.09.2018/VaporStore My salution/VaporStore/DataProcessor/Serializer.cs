namespace VaporStore.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using VaporStore.Data.Models.Enumerations;
    using VaporStore.DTOS.Export;

    public static class Serializer
    {
        public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
        {
            var genresDto = context
                                .Genres
                                .Include(x => x.Games)
                                .ThenInclude(g => g.Purchases)
                                .Where(x => genreNames.Contains(x.Name))
                                .Select(x => new export_genreGames_dto()
                                {
                                    Id = x.Id,
                                    Genre = x.Name,
                                    Games = x.Games.Where(g => g.Purchases.Count > 0)
                                    .Select(g => new game_exp_dto()
                                    {
                                        Id = g.Id,
                                        Name = g.Name,
                                        DeveloperName = g.Developer.Name,
                                        Players = g.Purchases.Count,
                                        Tags = string.Join(", ", g.GameTags.Select(t => t.Tag.Name))
                                    }).OrderByDescending(g => g.Players).ThenBy(g=>g.Id).ToArray(),
                                    TotalPlayers = x.Games.Sum(g => g.Purchases.Count)
                                })
                                .OrderByDescending(x => x.TotalPlayers).ThenBy(x => x.Id)
                                .ToArray();

            string resultJson = JsonConvert.SerializeObject(genresDto, Formatting.Indented);

            return resultJson;
        }

        public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
        {
            Console.ReadLine();
            var serializer = new XmlSerializer(typeof(export_userXML_dto[]), new XmlRootAttribute("Users"));

            PurchaseType purchaseType = Enum.Parse<PurchaseType>(storeType);
            var usersSet = context.Users
                .Include(x => x.Cards)
                .ThenInclude(c => c.Purchases)
                .ThenInclude(p => p.Game)
                .Where(x => x.Cards.Any(c => c.Purchases.Any(p => p.Type == purchaseType)))
                .Select(x => new export_userXML_dto()
                {
                    Username = x.Username,
                    TotalSpent = x.Cards
                                        .SelectMany(c => c.Purchases)
                                        .Where(p => p.Type == purchaseType)
                                        .Sum(p => p.Game.Price),
                    Purchases = x.Cards
                                       .SelectMany(c => c.Purchases)
                                       .Where(p => p.Type == purchaseType)
                                       .OrderBy(p=>p.Date)
                                       .Select(p => new export_purchaseXML_dto()
                                       {
                                           Card = p.Card.Number,
                                           Cvc=p.Card.Cvc,
                                           Date=p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                                           Game=new export_gameXML_dto()
                                           {
                                               GameName=p.Game.Name,
                                               Genre=p.Game.Genre.Name,
                                               Price=p.Game.Price
                                           }
                                       }).ToList()
                })
                .OrderByDescending(x => x.TotalSpent)
                .ThenBy(x => x.Username).ToArray();

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            StringBuilder sb = new StringBuilder();

            using (StringWriter sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, usersSet, ns);
            }
            return sb.ToString().Trim();

        }
    }
}