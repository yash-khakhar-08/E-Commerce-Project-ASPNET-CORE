using MarketMatrix.Repository;
using MarketMatrix_DataAccess;
using MarketMatrix_Models;
using MarketMatrix_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketMatrix.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly CategoryRepo _categoryRepo;

        public CategoryController(CategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public IActionResult Index()
        {
            List<Category> categories = _categoryRepo.GetAll().ToList();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            List<Category> categories = _categoryRepo.GetAll().ToList();
            if (categories.Any(c => c.Name == category.Name && c.SectionName == category.SectionName))
            {
                // here name is key that must be same as input type validation-for tag
                ModelState.AddModelError("name", "Category is already present");
            }

            if (ModelState.IsValid)
            {
                
                _categoryRepo.Add(category);
                _categoryRepo.Save();
                TempData["success"] = "Category Added";
                return RedirectToAction("Index", "Category");
            }
            return View();

        }

        public IActionResult Edit(int id)
        {

            Category category = _categoryRepo.GetById(id);
            if (category != null)
            {
                return View("Edit", category);
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {

            if (category != null)
            {
                _categoryRepo.Update(category);
                TempData["success"] = "Category Updated";
                _categoryRepo.Save();
            }

            return RedirectToAction("Index", "Category");

        }

        public IActionResult Delete(int id)
        {

            Category category = _categoryRepo.GetById(id);
            if (category != null)
            {
                _categoryRepo.Delete(category);
                TempData["success"] = "Category Deleted";
                _categoryRepo.Save();
            }
            return RedirectToAction("Index", "Category");

        }


    }
}
