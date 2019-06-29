using Newtonsoft.Json;
using System.Collections.Generic;

namespace VaporStore.DTOS.Export
{
    public class export_genreGames_dto
    {
        public export_genreGames_dto()
        {
            Games = new List<game_exp_dto>();
        }
        public int Id { get; set; }
        public string Genre { get; set; }
        public virtual ICollection<game_exp_dto> Games { get; set; }

        public int TotalPlayers { get; set; }
    }

    public class game_exp_dto
    {
        public int Id { get; set; }
        [JsonProperty(PropertyName = "Title")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "Developer")]
        public string DeveloperName { get; set; }

        public string Tags { get; set; }
        public int Players { get; set; }

    }
}
//        "Id": 49,
//        "Title": "Warframe",
//        "Developer": "Digital Extremes",
//        "Tags": "Single-player, In-App Purchases, Steam Trading Cards, Co-op, Multi-player, Partial Controller Support",
//        "Players": 6



//[
//  {
//    "Id": 4,
//    "Genre": "Violent",
//    "Games": [
//      {
//        "Id": 49,
//        "Title": "Warframe",
//        "Developer": "Digital Extremes",
//        "Tags": "Single-player, In-App Purchases, Steam Trading Cards, Co-op, Multi-player, Partial Controller Support",
//        "Players": 6
//      },
//      {
//        "Id": 22,
//        "Title": "Soul at Stake",
//        "Developer": "Chongming Studio",
//        "Tags": "Co-op, Multi-player, Online Multi-Player, Steam Cloud, Online Co-op",
//        "Players": 2
//      },
//      {
//        "Id": 40,
//        "Title": "Black Desert Online",
//        "Developer": "Pearl Abyss",
//        "Tags": "In-App Purchases, Steam Trading Cards, Online Multi-Player, Online Co-op, MMO, Partial Controller Support",
//        "Players": 1
