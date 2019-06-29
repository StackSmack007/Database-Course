namespace CarDealer.Dtos.Import
{
    using System;
    using System.Xml.Serialization;

    [XmlType("Customer")]
    public class imp_customer_dto
    {

        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("birthDate")]
        public DateTime BirthDate { get; set; }
        [XmlElement("isYoungDriver")]
        public bool IsYoungDriver { get; set; }

      // <Customer>
      //   <name>Marcelle Griego</name>
      //   <birthDate>1990-10-04T00:00:00</birthDate>
      //   <isYoungDriver>true</isYoungDriver>
      // </Customer>

    }
}