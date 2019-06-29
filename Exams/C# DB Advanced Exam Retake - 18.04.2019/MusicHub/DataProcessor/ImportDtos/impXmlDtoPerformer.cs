namespace MusicHub.DataProcessor.ImportDtos
{
    using System.Collections.Generic;
    using System.Xml.Serialization;
    [XmlType("Performer")]
    public class impXmlDtoPerformer
    {

        [XmlElement]
        public string FirstName { get; set; }

        [XmlElement]
        public string LastName { get; set; }

        [XmlElement]
        public int Age { get; set; }

        [XmlElement]
        public decimal NetWorth { get; set; }

        [XmlArray("PerformersSongs")]
        public virtual List<impXmlDtoSongId> SongIds { get; set; } = new List<impXmlDtoSongId>();

    }

    [XmlType("Song")]
    public class impXmlDtoSongId
    {
        [XmlAttribute("id")]
        public int SongId { get; set; }
    }
}

//<Performer>
//    <FirstName>Peter</FirstName>
//    <LastName>Bree</LastName>
//    <Age>25</Age>
//    <NetWorth>3243</NetWorth>
//    <PerformersSongs>
//      <Song id = "2" />
//      < Song id="1" />
//    </PerformersSongs>
