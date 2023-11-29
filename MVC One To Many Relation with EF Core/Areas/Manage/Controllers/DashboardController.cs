using Microsoft.AspNetCore.Mvc;
using MVC_One_To_Many_Relation_with_EF_Core.DAL;

namespace MVC_One_To_Many_Relation_with_EF_Core.Areas.Manage.Controllers
{
    [Area("Manage")]
    
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
