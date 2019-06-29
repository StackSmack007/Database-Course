namespace MusicHub.Data.Models
{
    using MusicHub.Data.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Song : BaseId
    {
        public Song()
        {
            SongPerformers = new HashSet<SongPerformer>();
        }

        [Required, MinLength(3), MaxLength(20)]
        public string Name { get; set; }                                  //– text with min length 3 and max length 20 (required)

        public TimeSpan Duration { get; set; }                            //– TimeSpan(required)

        public DateTime CreatedOn { get; set; }                           //– Date(required)

        public Genre Genre { get; set; }                                  //¬– Genre enumeration with possible values: "Blues, Rap, PopMusic, Rock, Jazz" (required)

        public int? AlbumId { get; set; }                                 //– integer foreign key
        [ForeignKey(nameof(AlbumId))]
        public Album Album { get; set; }                                  //– the song’s album

        public int WriterId { get; set; }                                 // - integer, foreign key(required)
        [ForeignKey(nameof(WriterId))]
        public Writer Writer { get; set; }                                // – the song’s writer

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }                                // – decimal (non-negative, minimum value: 0) (required)

        public ICollection<SongPerformer> SongPerformers { get; set; }    // - collection of type SongPerformer
    }
}