namespace ProductShop.Dtos.Import
{
    using System.Xml.Serialization;
    [XmlType("Category")]
    public class categoryImport_dto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        //<name>Electronics</name>
    }
}