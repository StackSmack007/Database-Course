namespace ProductShop.DTOS.UsersAndProducts
{
using System.Collections.Generic;
    public class usersAndProductsDTO
    {
        public usersAndProductsDTO()
        {
            users = new List<user_dto>();
        }
        public int usersCount => users.Count;
        public ICollection<user_dto> users { get; set; }
    }
}