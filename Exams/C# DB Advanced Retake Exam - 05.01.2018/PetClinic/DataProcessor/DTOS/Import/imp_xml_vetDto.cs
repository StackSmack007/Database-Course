namespace PetClinic.DataProcessor.DTOS.Import
{
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
    [XmlType("Vet")]
   public class imp_xml_vetDto
    {
        [XmlElement]
        [Required, MinLength(3), MaxLength(40)]
        public string Name { get; set; }

        [XmlElement]
        [Required, MinLength(3), MaxLength(50)]
        public string Profession { get; set; }

        [XmlElement]
        [Range(22, 65)]
        public int Age { get; set; }

        [XmlElement]
        [Required, RegularExpression(@"^((\+359)|0)\d{9}$")]
        public string PhoneNumber { get; set; }
    }
}
//<Vet>
//        <Name>Michael Jordan</Name>
//        <Profession>Emergency and Critical Care</Profession>
//        <Age>45</Age>
//        <PhoneNumber>0897665544</PhoneNumber>
//</Vet>