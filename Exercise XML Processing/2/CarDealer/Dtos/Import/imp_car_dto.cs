namespace CarDealer.Dtos.Import
{
    using System.Xml.Serialization;

    [XmlType("Car")]
    public class imp_car_dto
    {
        [XmlElement("make")]
        public string Make { get; set; }
        [XmlElement("model")]
        public string Model { get; set; }
        [XmlElement("TraveledDistance")]
        public long TravelledDistance { get; set; }

        [XmlArray("parts")]
        public PartId[] PartIds { get; set; }


        //       <Cars>
        // <Car>
        //   <make>Opel</make>
        //   <model>Omega</model>
        //   <TraveledDistance>176664996</TraveledDistance>
        //   <parts>
        //     <partId id = "38" />
        //     < partId id="102"/>
        //     <partId id = "23" />
        //     < partId id="116"/>
        //     <partId id = "46" />
        //     < partId id="68"/>
        //     <partId id = "88" />
        //     < partId id="104"/>
        //     <partId id = "71" />
        //     < partId id="32"/>
        //     <partId id = "114" />
        //   </ parts >
        // </ Car >
        // < Car >
    }
    [XmlType("partId")]
    public class PartId
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
