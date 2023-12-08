using Microsoft.AspNetCore.Mvc;
using MoviesWeb.Data;
using MoviesWeb.Models;

namespace MoviesWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db) 
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order can't match the name");
            }
            if (ModelState.IsValid) 
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id==0)
            {
                TempData["error"] = "Failed to Creat Category";
                return NotFound();
            }
            Category? category = _db.Categories.Find(id);
            /*            Category? category1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
                        Category? category2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();*/
            if (category == null)
            {
                TempData["error"] = "Failed to Creat Category";
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
           
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Edited successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = "Failed to Delete Category";
                return NotFound();
            }
            Category? category = _db.Categories.Find(id);
            if (category == null)
            {
                TempData["error"] = "Failed to Delete Category";
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                TempData["error"] = "Failed to Delete Category";
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
