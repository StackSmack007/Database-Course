using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace VaporStore.DTOS
{
    [XmlType("Purchase")]
    public class xmlPurchase_inp_dto
    {
        [XmlAttribute("title")]
        [Required]
        public string GameName { get; set; }

        [XmlElement("Type")]
        [Required]//, RegularExpression(@"(^Digital$)|(^Retail$)")]
        public string PurchaseType { get; set; }

        [XmlElement("Key")]
        [Required, RegularExpression(@"^([A-Z\d]{4}-){2}[A-Z\d]{4}$")]
        public string ProducTtKey { get; set; }

        [XmlElement("Card")]
        [Required, RegularExpression(@"^(\d{4}\s){3}\d{4}$")]
        public string CardNumber { get; set; }

        [XmlElement("Date")]
        [Required]
        public string DateOfPurchase { get; set; }

      

    }
}
