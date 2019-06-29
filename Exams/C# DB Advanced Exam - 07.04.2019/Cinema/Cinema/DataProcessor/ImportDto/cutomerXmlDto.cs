namespace Cinema.DataProcessor.ImportDto
{
    using System.Collections.Generic;
    using System.Xml.Serialization;
    [XmlType("Customer")]
    public class cutomerXmlDto
    {

        [XmlElement]
        public string FirstName { get; set; }

        [XmlElement]
        public string LastName { get; set; }

        [XmlElement]
        public int Age { get; set; }

        [XmlElement]
        public decimal  Balance { get; set; }
        [XmlArray]
        public List<ticketXmlDto> Tickets { get; set; } = new List<ticketXmlDto>();
    }
}
