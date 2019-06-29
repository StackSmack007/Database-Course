namespace SoftJail.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    [XmlType("Officer")]
    public class impOfficerAndPrisonerDto
    {

        [XmlElement("Name")]
        [Required, MinLength(3), MaxLength(30)]
        public string FullName { get; set; }

        [XmlElement("Money")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Salary { get; set; }

        [XmlElement]
        [Required]
        public string Position { get; set; }

        [XmlElement]
        [Required]
        public string Weapon { get; set; }

        [XmlElement]
        public int DepartmentId { get; set; }

        [XmlArray]
        public xmlPrisonerDto[] Prisoners { get; set; }

        //        <Officer>
        //  <Name>Minerva Kitchingman</Name>
        //  <Money>2582</Money>
        //  <Position>Invalid</Position>
        //  <Weapon>ChainRifle</Weapon>
        //  <DepartmentId>2</DepartmentId>
        //  <Prisoners>
        //    <Prisoner id = "15" />
        //  </ Prisoners >

    }

    [XmlType("Prisoner")]
    public class xmlPrisonerDto
    {
        [XmlAttribute("id")]
        public int PrisonerId { get; set; }
    }
}
