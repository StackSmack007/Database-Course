namespace MusicHub.DataProcessor.ImportDtos
{
using System.Xml.Serialization;
    [XmlType("Song")]
    public class impXmlDtoSong
    {
        [XmlElement]
        public string Name { get; set; }
        [XmlElement]
        public string Duration { get; set; }
        [XmlElement]
        public string CreatedOn { get; set; }
        [XmlElement]
        public string Genre { get; set; }
        [XmlElement]
        public int? AlbumId { get; set; }
        [XmlElement]
        public int WriterId { get; set; }
        [XmlElement]
        public decimal Price { get; set; }
    }
}
//<Song>
//    <Name>What Goes Around</Name>
//    <Duration>00:03:23</Duration>
//    <CreatedOn>21/12/2018</CreatedOn>
//    <Genre>Blues</Genre>
//    <AlbumId>2</AlbumId>
//    <WriterId>2</WriterId>
//    <Price>12</Price>
//  </Song>