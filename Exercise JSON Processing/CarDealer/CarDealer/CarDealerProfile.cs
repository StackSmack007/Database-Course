using AutoMapper;
using CarDealer.DTO;
using CarDealer.DTO.Task_5;
using CarDealer.DTO.Task_6;
using CarDealer.DTO.task_7;
using CarDealer.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {

            CreateMap<car_in_dto, Car>();
            CreateMap<Customer, customer_id_isYoungDTO>();
            CreateMap<Customer, customer_DTO_out>()
                .ForMember(d=>d.BirthDate,opt=>opt.MapFrom(s=>s.BirthDate.ToString("dd/MM/yyyy",CultureInfo.InvariantCulture)));

            CreateMap<Car, car_DTOout>();




        }

    }
}
