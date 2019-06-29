using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ExportDto
{

    [XmlType("Prisoner")]
    public class xmlExp_inboxForPrisoner
    {
        [XmlElement]
        public int Id { get; set; }

        [XmlElement("Name")]
        public string FullName { get; set; }

        [XmlElement]
        public string IncarcerationDate { get; set; }

        [XmlArray("EncryptedMessages")]
        public  List<xmlMail> Mails { get; set; } = new List<xmlMail>();
    }
}