namespace CarDealer.Dtos.Import
{
    using System.Xml.Serialization;

    [XmlType("Sale")]
    public class imp_sale_dto
    {

        [XmlElement("carId")]
        public int CarId { get; set; }

        [XmlElement("customerId")]
        public int CustomerId { get; set; }
        [XmlElement("discount")]
        public decimal Discount { get; set; }


      // <carId>234</carId>
      // <customerId>23</customerId>
      // <discount>50</discount>
    }
}