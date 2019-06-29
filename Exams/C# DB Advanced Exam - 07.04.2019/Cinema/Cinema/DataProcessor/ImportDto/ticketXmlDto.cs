using System.Xml.Serialization;

namespace Cinema.DataProcessor.ImportDto
{
    [XmlType("Ticket")]
    public class ticketXmlDto
    {
       [XmlElement]
        public decimal Price { get; set; }
        [XmlElement]
        public int ProjectionId { get; set; }
    }
}


//<Customer>
//    <FirstName>Randi</FirstName>
//    <LastName>Ferraraccio</LastName>
//    <Age>20</Age>
//    <Balance>59.44</Balance>
//    <Tickets>
//      <Ticket>
//        <ProjectionId>1</ProjectionId>
//        <Price>7</Price>
//      </Ticket>