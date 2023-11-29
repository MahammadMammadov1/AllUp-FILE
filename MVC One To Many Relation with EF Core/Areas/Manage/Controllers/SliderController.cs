using Microsoft.AspNetCore.Mvc;
using MVC_One_To_Many_Relation_with_EF_Core.DAL;
using MVC_One_To_Many_Relation_with_EF_Core.Models;


namespace Pustok.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _slider;

        public SliderController(AppDbContext appDb)
        {
            _slider = appDb;
        }
        public IActionResult Index()
        {
            List<Slider> sliderList = _slider.Sliders.ToList();
            return View(sliderList);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if (!ModelState.IsValid) return View();
            if (slider.IFormFile != null)
            {
                string fileName = slider.IFormFile.FileName;
                if (slider.IFormFile.ContentType != "image/jpeg" && slider.IFormFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("IFormFile", "you can only add png or jpeg file");
                    return View();
                }

                if (slider.IFormFile.Length > 1048576)
                {
                    ModelState.AddModelError("IFormFile", "file must be lower than 1 mb");
                    return View();
                }

                if (slider.IFormFile.FileName.Length > 64)
                {
                    fileName = fileName.Substring(fileName.Length - 64, 64);
                }

                fileName = Guid.NewGuid().ToString() + fileName;

                string path = "C:\\Users\\Mehemmed\\Desktop\\MVC-Slider\\MVC One To Many Relation with EF Core\\wwwroot\\upload\\sliders\\" + fileName;
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    slider.IFormFile.CopyTo(fileStream);
                }

                slider.Image = fileName;
            }
            else
            {
                ModelState.AddModelError("IFormFile", "Image is required!");
                return View();
            }


            _slider.Sliders.Add(slider);
            _slider.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var wanted = _slider.Sliders.FirstOrDefault(x => x.Id == id);

            return View(wanted);
        }
        [HttpPost]
        public IActionResult Update(Slider slider)
        {
            if(!ModelState.IsValid) return View(slider);
            var wanted = _slider.Sliders.FirstOrDefault(x => x.Id == slider.Id);

            if (wanted == null)
            {
                return NotFound();
            }

            string oldFilePath = "C:\\Users\\Mehemmed\\Desktop\\MVC-Slider\\MVC One To Many Relation with EF Core\\wwwroot\\upload\\sliders\\" + wanted.Image;

            if (slider.IFormFile != null)
            {

                string newFileName = slider.IFormFile.FileName;
                if (slider.IFormFile.ContentType != "image/jpeg" && slider.IFormFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("IFormFile", "you can only add png or jpeg file");
                    return View() ;
                }

                if (slider.IFormFile.Length > 1048576)
                {
                    ModelState.AddModelError("IFormFile", "file must be lower than 1 mb");
                    return View();

                }

                if (slider.IFormFile.FileName.Length > 64)
                {
                    newFileName = newFileName.Substring(newFileName.Length - 64, 64);
                }

                newFileName = Guid.NewGuid().ToString() + newFileName;

                string newFilePath = "C:\\Users\\Mehemmed\\Desktop\\MVC-Slider\\MVC One To Many Relation with EF Core\\wwwroot\\upload\\sliders\\" + newFileName;
                using (FileStream fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    slider.IFormFile.CopyTo(fileStream);
                }

                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }

                wanted.Image = newFileName;
            }

            wanted.Title1 = slider.Title1;
            wanted.Title2 = slider.Title2;
            wanted.Title3 = slider.Title3;
            wanted.Description = slider.Description;
            wanted.RedirctUrl1 = slider.RedirctUrl1;

            _slider.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var wanted = _slider.Sliders.FirstOrDefault(x => x.Id == id);

            return View(wanted);
        }
        [HttpPost]
        public IActionResult Delete(Slider slider)
        {
            var sliderToDelete = _slider.Sliders.FirstOrDefault(x => x.Id == slider.Id);

            if (sliderToDelete == null)
            {
                return NotFound();
            }

            string filePath = "C:\\Users\\Mehemmed\\Desktop\\MVC-Slider\\MVC One To Many Relation with EF Core\\wwwroot\\upload\\sliders\\" + sliderToDelete.Image;

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            _slider.Sliders.Remove(sliderToDelete);
            _slider.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}
