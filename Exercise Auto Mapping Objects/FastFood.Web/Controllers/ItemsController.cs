namespace FastFood.Web.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using FastFood.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using ViewModels.Items;

    public class ItemsController : Controller
    {
        private readonly FastFoodContext context;
        private readonly IMapper mapper;

        public ItemsController(FastFoodContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            var categories = this.context.Categories
                .ProjectTo<CreateItemViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return this.View(categories);
        }

        [HttpPost]
        public IActionResult Create(CreateItemInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "Home");
            }
            Item item = mapper.Map<Item>(model);
            context.Items.Add(item);
            context.SaveChanges();
            return RedirectToAction("All","Items");

        }

        public IActionResult All()
        {
            var allItems = context.Items.ProjectTo<ItemsAllViewModels>(mapper.ConfigurationProvider).ToArray();
            return View(allItems);
        }
    }
}