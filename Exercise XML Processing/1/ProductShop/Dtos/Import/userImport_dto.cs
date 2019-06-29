namespace ProductShop.Dtos.Import
{
    using System.Xml.Serialization;
    [XmlType("Category")]
    public class userImport_dto
    {

        [XmlElement("firstName")]
        public string FirstName { get; set; }
        [XmlElement("lastName")]
        public string LastName { get; set; }
        [XmlElement("age")]
        public int Age { get; set; }

        // <User>
        // <firstName>Etty</firstName>
        // <lastName>Haill</lastName>
        // <age>31</age>
        // </User>
    }
}