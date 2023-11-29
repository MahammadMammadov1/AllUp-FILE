using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_One_To_Many_Relation_with_EF_Core.DAL;
using MVC_One_To_Many_Relation_with_EF_Core.Models;

namespace MVC_One_To_Many_Relation_with_EF_Core.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDb;

        public ProductController(AppDbContext appDb)
        {
            _appDb = appDb;
        }
        public IActionResult Index()
        {

            List<Product> products = _appDb.Products.ToList();
            return View(products);
        }
        public IActionResult Create()
        {
            
            ViewBag.Tags = _appDb.Tags.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            
            ViewBag.Tags = _appDb.Tags.ToList();

            if (!ModelState.IsValid) return View(product);
            
            var check = false;
            if (product.TagIds != null)
            {
                foreach (var tagId in product.TagIds)
                {
                    if (!_appDb.Tags.Any(x => x.Id == tagId))
                        check = true;
                }
            }
            if (check)
            {
                ModelState.AddModelError("TagId", "Tag not found!");
                return View(product);
            }
            else
            {
                if (product.TagIds != null)
                {
                    foreach (var tagId in product.TagIds)
                    {
                        ProductTag bookTag = new ProductTag
                        {
                            Product = product,
                            TagId = tagId
                        };
                        _appDb.ProductTags.Add(bookTag);
                    }
                }
            }
            if (product.ProductPoster != null)
            {
                string fileName = product.ProductPoster.FileName;
                if (product.ProductPoster.ContentType != "image/jpeg" && product.ProductPoster.ContentType != "image/png")
                {
                    ModelState.AddModelError("ProductPoster", "you can only add png or jpeg file");
                }

                if (product.ProductPoster.Length > 1048576)
                {
                    ModelState.AddModelError("ProductPoster", "file must be lower than 1 mb");
                }

                if (product.ProductPoster.FileName.Length > 64)
                {
                    fileName = fileName.Substring(fileName.Length - 64, 64);
                }

                fileName = Guid.NewGuid().ToString() + fileName;

                string path = "C:\\Users\\Mehemmed\\Desktop\\MVC-Slider\\MVC One To Many Relation with EF Core\\wwwroot\\upload\\" + fileName;
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    product.ProductPoster.CopyTo(fileStream);
                }

                ProductImage productImage = new ProductImage
                {
                    Product = product,
                    ImageUrl = fileName,
                    isPoster = true,
                };

                _appDb.ProductImages.Add(productImage);

            }
            else
            {
                ModelState.AddModelError("ProductPoster", "image is required");
            }



            if (product.ProductHower != null)
            {
                string fileName = product.ProductHower.FileName;
                if (product.ProductHower.ContentType != "image/jpeg" && product.ProductHower.ContentType != "image/png")
                {
                    ModelState.AddModelError("ProductHower", "you can only add png or jpeg file");
                }

                if (product.ProductHower.Length > 1048576)
                {
                    ModelState.AddModelError("ProductHower", "file must be lower than 1 mb");
                }

                if (product.ProductHower.FileName.Length > 64)
                {
                    fileName = fileName.Substring(fileName.Length - 64, 64);
                }

                fileName = Guid.NewGuid().ToString() + fileName;

                string path = "C:\\Users\\Mehemmed\\Desktop\\MVC-Slider\\MVC One To Many Relation with EF Core\\wwwroot\\upload\\" + fileName;
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    product.ProductHower.CopyTo(fileStream);
                }

                ProductImage productImage = new ProductImage
                {
                    Product = product,
                    ImageUrl = fileName,
                    isPoster = false,
                };
                _appDb.ProductImages.Add(productImage);
            }
            else
            {
                ModelState.AddModelError("ProductHower", "image is required");
            }


            if (product.ImageFiles != null)
            {
                foreach (var img in product.ImageFiles)
                {
                    string fileName = img.FileName;
                    if (img.ContentType != "image/jpeg" && img.ContentType != "image/png")
                    {
                        ModelState.AddModelError("ImageFiles", "you can only add png or jpeg file");
                    }

                    if (img.Length > 1048576)
                    {
                        ModelState.AddModelError("ImageFiles", "file must be lower than 1 mb");
                    }

                    if (img.FileName.Length > 64)
                    {
                        fileName = fileName.Substring(fileName.Length - 64, 64);
                    }

                    fileName = Guid.NewGuid().ToString() + fileName;

                    string path = "C:\\Users\\Mehemmed\\Desktop\\MVC-Slider\\MVC One To Many Relation with EF Core\\wwwroot\\upload\\" + fileName;
                    using (FileStream fileStream = new FileStream(path, FileMode.Create))
                    {
                        img.CopyTo(fileStream);
                    }

                    ProductImage productImage = new ProductImage
                    {
                        Product = product,
                        ImageUrl = fileName,
                        isPoster = null,
                    };
                    _appDb.ProductImages.Add(productImage);
                }
            }

            _appDb.Products.Add(product);
            _appDb.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            
            ViewBag.Tags = _appDb.Tags.ToList();

            if (!ModelState.IsValid) return View();
            var existProduct = _appDb.Products.Include(x=>x.ProductTag).FirstOrDefault(x => x.Id == id);
            return View(existProduct);
        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            
            ViewBag.Tags = _appDb.Tags.ToList();


            var existProduct = _appDb.Products.Include(x => x.ProductTag).Include(x => x.ProductImages).FirstOrDefault(b => b.Id == product.Id);
            if (existProduct == null) return NotFound();
            if (!ModelState.IsValid) return View(product);


            existProduct.ProductTag.RemoveAll(bt =>!product.TagIds.Contains(bt.TagId)); 

            foreach (var tagId in product.TagIds.Where(tagId => !existProduct.ProductTag.Any(pt => pt.TagId == tagId)))
            {
                ProductTag productTag = new ProductTag
                {
                    TagId = tagId
                };
                existProduct.ProductTag.Add(productTag);
            }


            if (product.ProductPoster != null)
            {
                string fileName = product.ProductPoster.FileName;
                if (product.ProductPoster.ContentType != "image/jpeg" && product.ProductPoster.ContentType != "image/png")
                {
                    ModelState.AddModelError("ProductPoster", "you can only add png or jpeg file");
                }

                if (product.ProductPoster.Length > 1048576)
                {
                    ModelState.AddModelError("ProductPoster", "file must be lower than 1 mb");
                }

                if (product.ProductPoster.FileName.Length > 64)
                {
                    fileName = fileName.Substring(fileName.Length - 64, 64);
                }

                fileName = Guid.NewGuid().ToString() + fileName;

                string path = "C:\\Users\\Mehemmed\\Desktop\\MVC-Slider\\MVC One To Many Relation with EF Core\\wwwroot\\upload\\" + fileName;
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    product.ProductPoster.CopyTo(fileStream);
                }

                ProductImage productImage = new ProductImage
                {
                    Product = product,
                    ImageUrl = fileName,
                    isPoster = true,
                };

                existProduct.ProductImages.Add(productImage);

            }
            else
            {
                ModelState.AddModelError("ProductPoster", "image is required");
            }



            if (product.ProductHower != null)
            {
                string fileName = product.ProductHower.FileName;
                if (product.ProductHower.ContentType != "image/jpeg" && product.ProductHower.ContentType != "image/png")
                {
                    ModelState.AddModelError("ProductHower", "you can only add png or jpeg file");
                }

                if (product.ProductHower.Length > 1048576)
                {
                    ModelState.AddModelError("ProductHower", "file must be lower than 1 mb");
                }

                if (product.ProductHower.FileName.Length > 64)
                {
                    fileName = fileName.Substring(fileName.Length - 64, 64);
                }

                fileName = Guid.NewGuid().ToString() + fileName;

                string path = "C:\\Users\\Mehemmed\\Desktop\\MVC-Slider\\MVC One To Many Relation with EF Core\\wwwroot\\upload\\" + fileName;
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    product.ProductHower.CopyTo(fileStream);
                }

                ProductImage productImage = new ProductImage
                {
                    Product = product,
                    ImageUrl = fileName,
                    isPoster = false,
                };
                existProduct.ProductImages.Add(productImage);
            }
            else
            {
                ModelState.AddModelError("ProductHower", "image is required");
            }

            existProduct.ProductImages.RemoveAll(bi => !product.ProductImageIds.Contains(bi.Id) && bi.isPoster == null);

            if (product.ImageFiles != null)
            {
                foreach (var img in product.ImageFiles)
                {
                    string fileName = img.FileName;
                    if (img.ContentType != "image/jpeg" && img.ContentType != "image/png")
                    {
                        ModelState.AddModelError("ImageFiles", "you can only add png or jpeg file");
                    }

                    if (img.Length > 1048576)
                    {
                        ModelState.AddModelError("ImageFiles", "file must be lower than 1 mb");
                    }

                    if (img.FileName.Length > 64)
                    {
                        fileName = fileName.Substring(fileName.Length - 64, 64);
                    }

                    fileName = Guid.NewGuid().ToString() + fileName;

                    string path = "C:\\Users\\Mehemmed\\Desktop\\MVC-Slider\\MVC One To Many Relation with EF Core\\wwwroot\\upload\\" + fileName;
                    using (FileStream fileStream = new FileStream(path, FileMode.Create))
                    {
                        img.CopyTo(fileStream);
                    }

                    ProductImage productImage = new ProductImage
                    {
                        Product = product,
                        ImageUrl = fileName,
                        isPoster = null,
                    };
                    existProduct.ProductImages.Add(productImage);
                }
            }

            existProduct.Name = product.Name;
            existProduct.Description = product.Description;
            existProduct.CostPrice = product.CostPrice;
            existProduct.DiscountedPrice = product.DiscountedPrice;
            existProduct.Code = product.Code;
            existProduct.SalePrice = product.SalePrice;
            existProduct.Tax = product.Tax;
            existProduct.IsAvailable = product.IsAvailable;
           
            _appDb.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var wanted = _appDb.Products.FirstOrDefault(x => x.Id == id);
            if (wanted == null) return NotFound();
            return View(wanted);
        }
        [HttpPost]
        public IActionResult Delete(Product product)
        {
            var wanted = _appDb.Products.FirstOrDefault(x => x.Id == product.Id);
            if (wanted == null) return NotFound();
            _appDb.Products.Remove(wanted);
            _appDb.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}