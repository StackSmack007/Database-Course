namespace VaporStore.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.Data.Models.Enumerations;
    using VaporStore.DTOS;

    public static class Deserializer
    {
        public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {

            jsonGame_inp_dto[] dtoInfo = JsonConvert.DeserializeObject<jsonGame_inp_dto[]>(jsonString);
            StringBuilder sb = new StringBuilder();

            List<Genre> genres = new List<Genre>();
            List<Developer> developers = new List<Developer>();
            List<Tag> tags = new List<Tag>();
            List<Game> games = new List<Game>();

            foreach (var dto in dtoInfo)
            {
                DateTime releaseDate;
                if (!AttributeValidation.IsValid(dto) || !dto.TagNames.Any() || !DateTime.TryParse(dto.ReleaseDate, out releaseDate))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                Genre genre;
                if (!genres.Any(x => x.Name == dto.Genre))
                {
                    genres.Add(new Genre() { Name = dto.Genre });
                }
                genre = genres.First(x => x.Name == dto.Genre);


                Developer developer;
                if (!developers.Any(x => x.Name == dto.DeveloperName))
                {
                    developers.Add(new Developer() { Name = dto.DeveloperName });
                }
                developer = developers.First(x => x.Name == dto.DeveloperName);

                List<GameTag> gameTags = new List<GameTag>();
                foreach (var tagName in dto.TagNames)
                {
                    if (!tags.Any(x => x.Name == tagName))
                    {
                        tags.Add(new Tag() { Name = tagName });
                    }
                    gameTags.Add(new GameTag() { Tag = tags.First(x => x.Name == tagName) });
                }

                games.Add(new Game()
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    ReleaseDate = releaseDate,
                    Developer = developer,
                    Genre = genre,
                    GameTags = gameTags
                });
                sb.AppendLine($"Added {dto.Name} ({dto.Genre}) with {dto.TagNames.Count} tags");
            }

            context.Games.AddRange(games);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            jsonUser_inp_dto[] usersDTOs = JsonConvert.DeserializeObject<jsonUser_inp_dto[]>(jsonString);
            StringBuilder sb = new StringBuilder();
            List<User> users = new List<User>();
            foreach (var dto in usersDTOs)
            {
                if (!AttributeValidation.IsValid(dto))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }
                var user = new User()
                {
                    FullName = dto.FullName,
                    Age = dto.Age,
                    Email = dto.Email,
                    Username = dto.Username,
                    Cards = dto.Cards.Select(x => new Card()
                    {
                        Cvc = x.Cvc,
                        Number = x.Number,
                        Type = (CardType)Enum.Parse(typeof(CardType), x.Type, true)
                    }).ToArray()
                };
                sb.AppendLine($"Imported {user.Username} with {user.Cards.Count} cards");
                users.Add(user);
            }
            context.Users.AddRange(users);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(xmlPurchase_inp_dto[]), new XmlRootAttribute("Purchases"));

            xmlPurchase_inp_dto[] purchasesDTOs = (xmlPurchase_inp_dto[])serializer.Deserialize(new StringReader(xmlString));

            var availableCards = context.Cards.Select(x => new
            {
                x.Id,
                x.Number,
                Owner = x.User.Username
            }).ToArray();

            var availableGames = context.Games.Select(x => new
            {
                x.Id,
                x.Name,
            }).ToArray();

            List<Purchase> purchasesToBeAdded = new List<Purchase>();

            StringBuilder sb = new StringBuilder();
            foreach (var dto in purchasesDTOs)
            {

                var cardInfo = availableCards.FirstOrDefault(x => x.Number == dto.CardNumber);
                int cardId = cardInfo.Id;
                var gameInfo = availableGames.FirstOrDefault(x => x.Name == dto.GameName);
                int gameId = gameInfo.Id;
                DateTime dateOfPurchase;
                PurchaseType typeOfPurchase;
                var productKey = dto.ProducTtKey;

                if (
                    !AttributeValidation.IsValid(dto) ||
                    cardInfo is null ||
                    gameInfo is null ||
                   !Enum.TryParse(dto.PurchaseType, true, out typeOfPurchase) ||
                    !DateTime.TryParse(dto.DateOfPurchase, out dateOfPurchase))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }
                Purchase new_purchase = new Purchase()
                {
                    CardId = cardId,
                    GameId = gameId,
                    Date = dateOfPurchase,
                    ProductKey = productKey,
                    Type = typeOfPurchase
                };
                sb.AppendLine($"Imported {gameInfo.Name} for {cardInfo.Owner}");
                purchasesToBeAdded.Add(new_purchase);
            }
            context.Purchases.AddRange(purchasesToBeAdded);
            context.SaveChanges();
            return sb.ToString().Trim();
        }
    }
}