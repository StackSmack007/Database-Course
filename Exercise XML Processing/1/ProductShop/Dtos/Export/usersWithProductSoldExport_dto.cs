namespace ProductShop.Dtos.Export
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("User")]
    public class usersWithProductSoldExport_dto
    {
        public usersWithProductSoldExport_dto()
        {
            SoldProducts = new HashSet<productExp1_dto>();
        }

        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlArray("soldProducts")]
        public HashSet<productExp1_dto> SoldProducts { get; set; }
    }

    [XmlType("Product")]
    public class productExp1_dto
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}