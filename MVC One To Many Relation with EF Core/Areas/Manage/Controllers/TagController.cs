using Microsoft.AspNetCore.Mvc;
using MVC_One_To_Many_Relation_with_EF_Core.DAL;
using MVC_One_To_Many_Relation_with_EF_Core.Models;

namespace MVC_One_To_Many_Relation_with_EF_Core.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TagController : Controller
    {
        private readonly AppDbContext _appDb;

        public TagController(AppDbContext appDb)
        {
            _appDb = appDb;
        }
        public IActionResult Index()
        {
            List<Tag> tags = _appDb.Tags.ToList();
            return View(tags);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Tag tag)
        {
            if (!ModelState.IsValid) return View(tag);
            if (_appDb.Tags.Any(x => x.Name.Trim().ToLower() == tag.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", "This tag already exist!");
                return View(tag);
            }
            _appDb.Tags.Add(tag);
            _appDb.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var wanted = _appDb.Tags.FirstOrDefault(x => x.Id == id);
            if (wanted == null) return NotFound();
            return View(wanted);
        }
        [HttpPost]
        public IActionResult Update(Tag tag)
        {
            var exist = _appDb.Tags.FirstOrDefault(x => x.Id == tag.Id);
            if (exist == null) return NotFound();
            if (ModelState.IsValid) return View(tag);
            if (_appDb.Tags.Any(x => x.Id != tag.Id && x.Name.ToLower().Trim() == tag.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", "This tag already exist!");
                return View(tag);
            }
            exist.Name = tag.Name;
            _appDb.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var wanted = _appDb.Tags.FirstOrDefault(x => x.Id == id);
            if (wanted == null) return NotFound();
            return View(wanted);
        }
        [HttpPost]
        public IActionResult Delete(Tag tag)
        {
            var wanted = _appDb.Tags.FirstOrDefault(x => x.Id == tag.Id);
            if (wanted == null) return NotFound();
            _appDb.Tags.Remove(wanted);
            _appDb.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
