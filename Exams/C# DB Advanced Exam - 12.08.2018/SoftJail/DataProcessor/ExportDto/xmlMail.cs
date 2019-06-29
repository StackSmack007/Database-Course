namespace SoftJail.DataProcessor.ExportDto
{
    using System.Xml.Serialization;
    [XmlType("Message")]
    public class xmlMail
    {
        [XmlElement]
        public string Description { get; set; }
    }
}