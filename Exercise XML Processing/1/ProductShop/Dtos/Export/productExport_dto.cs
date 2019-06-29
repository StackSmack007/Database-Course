namespace ProductShop.Dtos.Export
{
    using System.Xml.Serialization;
    [XmlType("Product")]
    public class productExport_dto
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("buyer")]
        public string BuyerName { get; set; }
      //  <name>TRAMADOL HYDROCHLORIDE</name>
      //  <price>516.48</price>
      //   <buyer>Wallas Duffyn</buyer>
    }
}