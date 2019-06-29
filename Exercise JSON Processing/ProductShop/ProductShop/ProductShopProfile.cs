namespace ProductShop
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using ProductShop.DTOS;
    using ProductShop.DTOS.UsersAndProducts;
    using ProductShop.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            CreateMap<userDTOin, User>();
            CreateMap<productDTOin, Product>();

            CreateMap<categoryDTOin, Category>();

            CreateMap<User, userDTOut>()
                .ForMember(d => d.firstName, o => o.MapFrom(s => s.FirstName))
                .ForMember(d => d.lastName, o => o.MapFrom(s => s.LastName))
                .ForMember(d => d.soldProducts, o => o.MapFrom(s => s.ProductsSold.Where(x => x.Buyer != null).Select(x => new productDTOout_p()
                {
                    name = x.Name,
                    price = x.Price,
                    buyerFirstName = x.Buyer.FirstName,
                    buyerLastName = x.Buyer.LastName
                }).ToArray()));

            CreateMap<Category, categoryDTOut>()
                .ForMember(d => d.productsCount, opt => opt.MapFrom(s => s.CategoryProducts.Count))
                .ForMember(d => d.averagePrice, opt =>
                 opt.MapFrom(s => string.Format("{0:F2}", s.CategoryProducts.Sum(x => x.Product.Price) / s.CategoryProducts.Count)))
                .ForMember(d => d.totalRevenue, opt => opt.MapFrom(s => string.Format("{0:F2}", s.CategoryProducts.Sum(x => x.Product.Price))));



            CreateMap<ICollection<User>, usersAndProductsDTO>()
           .ForMember(d => d.users, o => o.MapFrom(s => s.AsQueryable().ProjectTo<user_dto>().ToArray()));

            CreateMap<User, user_dto>()
            .ForMember(d => d.ProductsSold, o => o.MapFrom(s => Mapper.Map<sold_products_dto>(s.ProductsSold)));

            CreateMap<ICollection<Product>, sold_products_dto>()
            .ForMember(d => d.products, o => o.MapFrom(s => s.AsQueryable().ProjectTo<product_dto>().ToArray()));

            CreateMap<Product, product_dto>();
        }
    }
}