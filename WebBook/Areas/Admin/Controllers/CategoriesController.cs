using Domin.Entity;
using Infarstuructre.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace WebBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IServicesRepository<Category> _servicesCategory;

        public CategoriesController(IServicesRepository<Category> servicesCategory)
        {
            _servicesCategory = servicesCategory;
        }
        public IActionResult Index()
        {
            var x = _servicesCategory.GetAll();
            return View();
        }
    }
}
