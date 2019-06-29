namespace Cinema.DataProcessor.ExportDto
{
    using System.Xml.Serialization;
    [XmlType("Customer")]
    public class expCustomerDtoXML
    {

        [XmlAttribute]
        public string FirstName { get; set; }

        [XmlAttribute]
        public string LastName { get; set; }

        [XmlElement]
        public string SpentMoney { get; set; }

        [XmlElement]
        public string SpentTime { get; set; }
    }
}
