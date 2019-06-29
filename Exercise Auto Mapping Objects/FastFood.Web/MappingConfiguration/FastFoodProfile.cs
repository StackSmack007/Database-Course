namespace FastFood.Web.MappingConfiguration
{
    using AutoMapper;
    using FastFood.Web.ViewModels.Categories;
    using FastFood.Web.ViewModels.Employees;
    using FastFood.Web.ViewModels.Items;
    using FastFood.Web.ViewModels.Orders;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels.Positions;

    public class FastFoodProfile : Profile
    {
        public FastFoodProfile()
        {
            #region Positions
            this.CreateMap<CreatePositionInputModel, Position>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.PositionName));

            this.CreateMap<Position, PositionsAllViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));
            #endregion

            #region CategoriesController
            CreateMap<CreateCategoryInputModel, Category>()
               .ForMember(x => x.Name, y => y.MapFrom(s => s.CategoryName))
               .ReverseMap();

            CreateMap<Category, CategoryAllViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));
            #endregion


            #region EmployeesController
            CreateMap<Position, RegisterEmployeeViewModel>()
                .ForMember(x => x.PositionId, y => y.MapFrom(s => s.Id));
                
            CreateMap<Employee, EmployeesAllViewModel>()
            .ForMember(x => x.Position, y => y.MapFrom(s => s.Position.Name));

            CreateMap<RegisterEmployeeInputModel, Employee>();
            #endregion


            #region ItemController

            CreateMap<Category, CreateItemViewModel>()
                .ForMember(x => x.CategoryId, y => y.MapFrom(s => s.Id));

            CreateMap<CreateItemInputModel, Item>();

            CreateMap<Item, ItemsAllViewModels>()
                .ForMember(x => x.Category, y => y.MapFrom(s => s.Category.Name));
            #endregion

            #region Orders

            CreateMap<CreateOrderInputModel, Order>()
                .ForMember(x => x.OrderItems, y => y.MapFrom(s => new List<OrderItem>()
                {
                    new OrderItem()
                                 {
                                     ItemId =s.ItemId,
                                     Quantity =s.Quantity
                                 }
                }))
                .ForMember(x => x.Type, y => y.MapFrom(s => s.OrderType));

            CreateMap<Order, OrderAllViewModel>()
                .ForMember(x => x.DateTime, y => y.MapFrom(s => DateTime.Now.ToString("dd-MM-yyyy H:mm:ss")))
                .ForMember(x => x.Employee, y => y.MapFrom(s => s.Employee.Name))
                .ForMember(x=>x.OrderType,y=>y.MapFrom(s=>s.Type))
                .ForMember(x=>x.OrderId,y=>y.MapFrom(s=>s.Id));
            #endregion
        }
    }
}