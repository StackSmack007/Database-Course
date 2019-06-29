using System.ComponentModel.DataAnnotations.Schema;

namespace VaporStore.Data.Models
{
    public class GameTag
    {

        public int GameId { get; set; }
         [ForeignKey(nameof(GameId))]
        public virtual Game Game { get; set; }

        public int TagId { get; set; }
        [ForeignKey(nameof(TagId))]
        public virtual Tag Tag { get; set; }
    }
}