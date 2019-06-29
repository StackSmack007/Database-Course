namespace CarDealer
{
    using AutoMapper;
    using CarDealer.Dtos.Export;
    using CarDealer.Dtos.Import;
    using CarDealer.Models;
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<imp_supplier_dto, Supplier>();
            CreateMap<imp_part_dto, Part>();
            CreateMap<imp_customer_dto, Customer>();
            CreateMap<imp_sale_dto, Sale>();
            CreateMap<Car, exp_carsWithDistance_dto>();
            CreateMap<Car, exp_BMWcar_dto>();
        }
    }
}