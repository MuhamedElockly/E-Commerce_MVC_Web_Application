using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

using MvcApp.Bulky.Models.Data;


namespace MvcApp.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _dbContext;
		public CategoryController(ApplicationDbContext db)
		{
			_dbContext = db;
		}


		public IActionResult Index()
		{
			List<Category> categories = _dbContext.Categories.ToList();
			return View(categories);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Category category)
		{
			if (category.Name == category.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", "The name can't be like display order");
			}
			if (ModelState.IsValid)
			{
				_dbContext.Categories.Add(category);
				_dbContext.SaveChanges();
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Category categoryFromDb = _dbContext.Categories.Find(id);
			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}
		[HttpPost]
		public IActionResult Edit(Category category)
		{
			if (category.Name == category.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", "The name can't be like display order");
			}
			if (ModelState.IsValid)
			{
				_dbContext.Categories.Update(category);
				_dbContext.SaveChanges();
				TempData["success"] = "Edit is successful";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Category categoryFromDb = _dbContext.Categories.Find(id);
			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			Category deletedCategory = _dbContext.Categories.Find(id);
			if (deletedCategory!=null)
			{
				_dbContext.Categories.Remove(deletedCategory); 
				_dbContext.SaveChanges();
				TempData["success"] = "Delete is successful";
			}

			return RedirectToAction("Index");
		}


	}
}
