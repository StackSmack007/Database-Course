namespace PetClinic.DataProcessor.DTOS.Export
{
    using System.Collections.Generic;
    using System.Xml.Serialization;
    [XmlType("Procedure")]
    public class exp_xml_procedureDto
    {
        [XmlElement("Passport")]
        public string PassportSerialNumber { get; set; }
        [XmlElement]
        public string OwnerNumber { get; set; }
        [XmlElement]
        public string DateTime { get; set; }
        [XmlArray]
        public virtual List<exp_xml_animalAid> AnimalAids { get; set; } = new List<exp_xml_animalAid>();
        [XmlElement]
        public decimal TotalPrice { get; set; }
    }
    [XmlType("AnimalAid")]
    public class exp_xml_animalAid
    {
        [XmlElement]
        public string Name { get; set; }
        [XmlElement]
        public decimal Price { get; set; }
    }
}