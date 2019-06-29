namespace CarDealer.Dtos.Import
{
    using System.Xml.Serialization;
    [XmlType("Part")]
    public class imp_part_dto
    {

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("quantity")]
        public int Quantity { get; set; }

        [XmlElement("supplierId")]
        public int SupplierId { get; set; }

        // <Part>
        // <name>Bonnet/hood</name>
        // <price>1001.34</price>
        // <quantity>10</quantity>
        // <supplierId>17</supplierId>
    }
}