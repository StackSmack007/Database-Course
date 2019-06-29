namespace ProductShop.DTOS
{
    using System.Collections.Generic;
    public class userDTOut
    {
        public userDTOut()
        {
            soldProducts = new List<productDTOout_p>();
        }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public ICollection<productDTOout_p> soldProducts { get; set; }
    }

    public class productDTOout_p
    {
        public string name { get; set; }
        public decimal price { get; set; }
        public string buyerFirstName { get; set; }
        public string buyerLastName { get; set; }
    }
}