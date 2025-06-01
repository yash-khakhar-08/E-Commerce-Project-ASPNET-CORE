using MarketMatrix.Repository;
using MarketMatrix.Service;
using MarketMatrix_Models;
using MarketMatrix_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace MarketMatrix.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
	{
		private readonly CategoryRepo _categoryRepo;
		private readonly ProductRepo _productRepo;
		private IWebHostEnvironment _iWebHostEnvironment;
		public ProductController(CategoryRepo categoryRepo, ProductRepo productRepo, IWebHostEnvironment iWebHostEnvironment)
		{
			_categoryRepo = categoryRepo;
			_productRepo = productRepo;
			_iWebHostEnvironment = iWebHostEnvironment;
		}
		public IActionResult Index()
		{

			List<Products> products = _productRepo.GetAll(includeProperties: "Category").ToList();


			return View();
		}

		public IActionResult Create()
		{
			IEnumerable<SelectListItem> Categories = _categoryRepo.GetAll().Select(u => new SelectListItem
			{
				Text = u.Name + "-" + u.SectionName,
				Value = u.Id.ToString()
			});

			ViewBag.Categories = Categories;

			return View();
		}

		[HttpPost]
		public IActionResult Create(Products products, IFormFile? file)
		{
			if (ModelState.IsValid)
			{

				if (file != null)
				{

					string wwwRootPath = _iWebHostEnvironment.WebRootPath;
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
					string productPath = Path.Combine(wwwRootPath, @"images\products");

					using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}

					products.ImageUrl = @"\images\products\" + fileName;

				}

				_productRepo.Add(products);
				_productRepo.Save();
				TempData["success"] = "Product Added Successfully!!";
				return RedirectToAction("Index");

			}

			return View();

		}

		[HttpDelete]
		public IActionResult Delete(int id) {

			Products products = _productRepo.GetById(id);
			if (products != null) {
				string wwwRootPath = _iWebHostEnvironment.WebRootPath;
				string productPath = Path.Combine(wwwRootPath, products.ImageUrl.TrimStart('\\'));
				if (System.IO.File.Exists(productPath))
				{
					System.IO.File.Delete(productPath);
				}

				_productRepo.Delete(products);
				_productRepo.Save();
			}
			return Json(new { success = true });

		}

		public IActionResult Edit(int id)
		{
			IEnumerable<SelectListItem> Categories = _categoryRepo.GetAll().Select(u => new SelectListItem
			{
				Text = u.Name + "-" + u.SectionName,
				Value = u.Id.ToString()
			});

			ViewBag.Categories = Categories;

			if (id != null && id != 0)
			{
				Products products = _productRepo.GetById(id);
				if (products != null)
				{
					return View(products);
				}
				else
				{
					return View("Index");
				}
			}
			else {
				return View("Index");
			}

		}

		[HttpPost]
		public IActionResult Edit(Products products, IFormFile? file)
		{
			try {

				IEnumerable<SelectListItem> Categories = _categoryRepo.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name + "-" + u.SectionName,
					Value = u.Id.ToString()
				});

				ViewBag.Categories = Categories;

				if (ModelState.IsValid) {

					if (file != null) {

						string wwwRootPath = _iWebHostEnvironment.WebRootPath;
						string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
						string productPath = Path.Combine(wwwRootPath, @"images\products");


						// remove image from folder
						string productPathToRemove = Path.Combine(wwwRootPath, products.ImageUrl.TrimStart('\\'));
						if (System.IO.File.Exists(productPathToRemove))
						{
							System.IO.File.Delete(productPathToRemove);
						}


						// copy new image to folder
						using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
						{
							file.CopyTo(fileStream);
						}

						products.ImageUrl = @"\images\products\" + fileName;

					}


					_productRepo.Update(products);
					_productRepo.Save();

					TempData["success"] = "Product Updated successfully!!";

					return RedirectToAction("Index");

				}

			}
			catch (Exception ex) {
				TempData["error"] = "Product Update failed!!";
			}

			return View();


		}

		//API section start

		[HttpGet]
		public IActionResult GetAll() {

			List<Products> products = _productRepo.GetAll(includeProperties: "Category").ToList();

			return Json(new { data = products});

		}

	}
}
