namespace ProductShop.Dtos.Export
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("Users")]
    public class userSoldProductsExportDTO
    {
        public userSoldProductsExportDTO()
        {
            Users = new List<userInfoAndSells_dto>();
        }

        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("users")]
        public List<userInfoAndSells_dto> Users { get; set; }
    }

    [XmlType("User")]
    public class userInfoAndSells_dto
    {

        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlElement("age")]
        public int? Age { get; set; }

        [XmlElement]
        public productsSolld_dto SoldProducts { get; set; }

    }

    [XmlType("SoldProducts")]
    public class productsSolld_dto
    {
        public productsSolld_dto()
        {
            Products = new List<productInfoExport_dto>();
        }

        [XmlElement("count")]
        public int ProductsCount { get; set; }

        [XmlArray("products")]
        public List<productInfoExport_dto> Products { get; set; }
    }

    [XmlType("Product")]
    public class productInfoExport_dto
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}