using System.Xml.Serialization;

namespace Cinema.DataProcessor.ImportDto
{
    [XmlType("Projection")]
    public class projectionXmlDto
    {
        [XmlElement]
        public int MovieId { get; set; }
        [XmlElement]
        public int HallId { get; set; }

        [XmlElement]
        public string DateTime { get; set; }

        // <Projection>
        //   <MovieId>38</MovieId>
        //   <HallId>4</HallId>
        //   <DateTime>2019-04-27 13:33:20</DateTime>
        // </Projection>
    }
}