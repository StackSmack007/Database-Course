namespace ProductShop.Models
{
    using System.Collections.Generic;
    using System.Xml.Serialization;
    [XmlRoot]
    public class User
    {
        public User()
        {
            this.ProductsSold = new List<Product>();
            this.ProductsBought = new List<Product>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? Age { get; set; }
        [XmlIgnore]
        public ICollection<Product> ProductsSold { get; set; }
        [XmlIgnore]
        public ICollection<Product> ProductsBought { get; set; }
    }
}