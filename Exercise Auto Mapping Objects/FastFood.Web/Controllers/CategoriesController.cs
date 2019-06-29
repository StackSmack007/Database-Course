namespace FastFood.Web.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using FastFood.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels.Categories;

    public class CategoriesController : Controller
    {
        private readonly FastFoodContext context;
        private readonly IMapper mapper;

        public CategoriesController(FastFoodContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "Home");
            }
            Category newCategory = mapper.Map<Category>(model);
            context.Categories.Add(newCategory);
            context.SaveChanges();
            return RedirectToAction("All", "Categories");
        }

        public IActionResult All()
        {
            IList<CategoryAllViewModel> categories = context.Categories.ProjectTo<CategoryAllViewModel>(mapper.ConfigurationProvider)
                .ToList();
            return View(categories);
        }
    }
}