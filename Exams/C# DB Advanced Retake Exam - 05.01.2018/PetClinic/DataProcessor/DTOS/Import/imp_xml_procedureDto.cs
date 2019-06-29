namespace PetClinic.DataProcessor.DTOS.Import
{
    using System.Collections.Generic;
    using System.Xml.Serialization;
    [XmlType("Procedure")]
    public class imp_xml_procedureDto
    {
        [XmlElement("Vet")]
        public string VetName { get; set; }

        [XmlElement("Animal")]
        public string AnimalPassportNumber { get; set; }

        [XmlElement]
        public string DateTime { get; set; }

        [XmlArray]
        public List<imp_xml_animalAid_Dto> AnimalAids { get; set; } = new List<imp_xml_animalAid_Dto>();


    }
    [XmlType("AnimalAid")]
    public class imp_xml_animalAid_Dto
    {
        [XmlElement]
        public string Name { get; set; }
    }
}
