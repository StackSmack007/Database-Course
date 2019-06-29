namespace MusicHub.DataProcessor.ExportDtos
{
using System.Xml.Serialization;
    [XmlType("Song")]
    public class expXmlSong
    {
        [XmlElement]
        public string SongName { get; set; }

        [XmlElement]
        public string Writer { get; set; }

        [XmlElement("Performer")]
        public string PerformerFullName { get; set; }

        [XmlElement]
        public string AlbumProducer { get; set; }

        [XmlElement]
        public string Duration { get; set; }
    }
} 