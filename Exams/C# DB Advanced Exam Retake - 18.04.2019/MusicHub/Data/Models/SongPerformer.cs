namespace MusicHub.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    public class SongPerformer //Mapping
    {
        public int SongId { get; set; }
        [ForeignKey(nameof(SongId))]
        public virtual Song Song { get; set; }

        public int PerformerId { get; set; }
        [ForeignKey(nameof(PerformerId))]
        public virtual Performer Performer { get; set; }
    }
}//done