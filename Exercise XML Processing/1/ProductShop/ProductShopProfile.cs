namespace ProductShop
{
    using AutoMapper;
    using ProductShop.Dtos.Export;
    using ProductShop.Dtos.Import;
    using ProductShop.Models;
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            CreateMap<userImport_dto, User>();
            CreateMap<productImport_dto, Product>();
            CreateMap<categoryImport_dto, Category>();
            CreateMap<categoryProductImport_dto, CategoryProduct>();
            CreateMap<Product, productExport_dto>()
                .ForMember(d=>d.BuyerName,opt=>opt.MapFrom(s=> s.Buyer==null?null:$"{s.Buyer.FirstName} {s.Buyer.LastName}"));
        }
    }
}