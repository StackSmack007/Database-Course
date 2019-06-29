namespace MusicHub.DataProcessor.ImportDtos
{
    using System.Collections.Generic;
    public class impJsonDtoProducer
    {
        public impJsonDtoProducer()
        {
            Albums = new List<impJsonDtoAlbum>();
        }
        public string Name { get; set; }

       
        public string Pseudonym { get; set; }

        public string PhoneNumber { get; set; }

        public virtual List<impJsonDtoAlbum> Albums { get; set; }
    }

    public class impJsonDtoAlbum
    {
        public string Name { get; set; }

        public string ReleaseDate { get; set; }
    }
}