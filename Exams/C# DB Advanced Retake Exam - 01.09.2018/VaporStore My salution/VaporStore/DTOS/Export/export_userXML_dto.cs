namespace VaporStore.DTOS.Export
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("User")]
    public class export_userXML_dto
    {
        [XmlAttribute("username")]
        public string Username { get; set; }

        [XmlArray()]
        public virtual List<export_purchaseXML_dto> Purchases { get; set; }

        [XmlElement()]
        public decimal TotalSpent { get; set; }
    }

    [XmlType("Purchase")]
    public class export_purchaseXML_dto
    {
        [XmlElement()]
        public string Card { get; set; }

        [XmlElement()]
        public string Cvc { get; set; }

        [XmlElement()]
        public string Date { get; set; }

        [XmlElement()]
        public export_gameXML_dto Game { get; set; }
    }

    [XmlType("Game")]
    public class export_gameXML_dto
    {
        [XmlAttribute("title")]
        public string GameName { get; set; }

        [XmlElement()]
        public string Genre { get; set; }

        [XmlElement()]
        public decimal Price { get; set; }
    }
}


//<Users>
//  <User username = "mgraveson" >
//    < Purchases >
//      < Purchase >
//        < Card > 7991 7779 5123 9211</Card>
//        <Cvc>340</Cvc>
//        <Date>2017-08-31 17:09</Date>
//        <Game title = "Counter-Strike: Global Offensive" >
//          < Genre > Action </ Genre >
//          < Price > 12.49 </ Price >
//        </ Game >
//      </ Purchase >
//      < Purchase >
//        < Card > 7790 7962 4262 5606</Card>
//        <Cvc>966</Cvc>
//        <Date>2018-02-28 08:38</Date>
//        <Game title = "Tom Clancy's Ghost Recon Wildlands" >
//          < Genre > Action </ Genre >
//          < Price > 59.99 </ Price >
//        </ Game >
//      </ Purchase >
//    </ Purchases >
//    < TotalSpent > 72.48 </ TotalSpent >
//  </ User >
