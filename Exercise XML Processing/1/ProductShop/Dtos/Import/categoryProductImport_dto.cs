namespace ProductShop.Dtos.Import
{
using System.Xml.Serialization;
    [XmlType("CategoryProduct")]
   public class categoryProductImport_dto
    {
        [XmlElement]
        public int CategoryId { get; set; }

        [XmlElement]
        public int ProductId { get; set; }
    }
}