using Microsoft.AspNetCore.Mvc;
using MVC_One_To_Many_Relation_with_EF_Core.DAL;
using MVC_One_To_Many_Relation_with_EF_Core.ViewModel;

namespace MVC_One_To_Many_Relation_with_EF_Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly DAL.AppDbContext _appDbContext;

        public HomeController(AppDbContext app1)
        {
            _appDbContext = app1;
        }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.Sliders = _appDbContext.Sliders.ToList();


            return View(homeViewModel);
        }
    }
}
