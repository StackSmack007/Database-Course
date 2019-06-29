using System.Collections.Generic;

namespace ProductShop.DTOS.UsersAndProducts
{
    public class sold_products_dto
    {
        public sold_products_dto()
        {
            products = new List<product_dto>();
        }
        public int count => products.Count;

        public ICollection<product_dto> products { get; set; }
    }
}
