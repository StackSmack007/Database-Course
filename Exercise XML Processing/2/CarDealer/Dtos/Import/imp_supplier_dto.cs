namespace CarDealer.Dtos.Import
{
using System.Xml.Serialization;
    [XmlType("Supplier")]
    public class imp_supplier_dto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("isImporter")]
        public bool IsImpproter { get; set; }
    }
}