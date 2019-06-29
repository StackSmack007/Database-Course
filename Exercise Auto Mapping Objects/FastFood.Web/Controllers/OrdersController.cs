namespace FastFood.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    using Data;
    using ViewModels.Orders;
    using FastFood.Models;
    using System.Collections.Generic;
    using AutoMapper.QueryableExtensions;

    public class OrdersController : Controller
    {
        private readonly FastFoodContext context;
        private readonly IMapper mapper;

        public OrdersController(FastFoodContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            var viewOrder = new CreateOrderViewModel
            {
                Items = this.context.Items.ToDictionary(x=>x.Id,x=>x.Name),
                Employees = this.context.Employees.ToDictionary(x => x.Id, x => x.Name),
            };

            return this.View(viewOrder);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "Home");
            }
            Order newOrder = mapper.Map<Order>(model);
            context.Orders.Add(newOrder);
            context.SaveChanges();
            return RedirectToAction("All", "Orders");
        }

        public IActionResult All()
        {
            IList<OrderAllViewModel> ordersAll = context.Orders.ProjectTo<OrderAllViewModel>(mapper.ConfigurationProvider).ToList();
            return View(ordersAll);
        }
    }
}
