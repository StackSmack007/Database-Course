namespace FastFood.DataProcessor.Dto.Import
{
    using System.Xml;
    using System.Xml.Serialization;
    [XmlType("Order")]
    public class dto_order_Xml
    {
        [XmlElement("Customer")]
        public string CustomerName { get; set; }

        [XmlElement("Employee")]
        public string EmployeeName { get; set; }

        [XmlElement]
        public string DateTime { get; set; }

        [XmlElement]
        public string Type { get; set; }

        [XmlArray]
        public dto_item_Xml[] Items { get; set; }
    }

    [XmlType("Item")]
    public class dto_item_Xml
    {
        [XmlElement]
        public string Name { get; set; }

        [XmlElement("Quantity")]
        public string QuantityINT { get; set; }
    }
}

