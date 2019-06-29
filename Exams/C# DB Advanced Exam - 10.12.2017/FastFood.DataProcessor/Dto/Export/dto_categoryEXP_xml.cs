namespace FastFood.DataProcessor.Dto.Export
{
using System.Xml.Serialization;
    [XmlType("Category")]
    public class dto_categoryEXP_xml
    {
        [XmlElement]
        public string Name { get; set; }
        [XmlElement]
        public dto_itemEXP_xml MostPopularItem { get; set; }
    }

    public class dto_itemEXP_xml
    {
        [XmlElement]
        public string Name { get; set; }
        [XmlElement]
        public decimal TotalMade { get; set; }
        [XmlElement]
        public int TimesSold { get; set; }
    }
}