namespace FastFood.Web.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using FastFood.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using ViewModels.Employees;

    public class EmployeesController : Controller
    {
        private readonly FastFoodContext context;
        private readonly IMapper mapper;

        public EmployeesController(FastFoodContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Register()
        {
            var positionsAvailable = context.Positions.ProjectTo<RegisterEmployeeViewModel>(mapper.ConfigurationProvider).ToList();

            return View(positionsAvailable);
        }

        [HttpPost]
        public IActionResult Register(RegisterEmployeeInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "Home");
            }
            context.Employees.Add(mapper.Map<Employee>(model));
            context.SaveChanges();
            return RedirectToAction("All", "Employees");

        }

        public IActionResult All()
        {
            var emps = context.Employees.ProjectTo<EmployeesAllViewModel>(mapper.ConfigurationProvider).ToList();
            return View(emps);
        }
    }
}
