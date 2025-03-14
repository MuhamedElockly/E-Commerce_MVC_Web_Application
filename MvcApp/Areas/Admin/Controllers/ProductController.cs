using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public ProductController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Add()
		{
			IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll()
				.Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()

				});
			ViewBag.CategoryList = CategoryList;
			return View();
		}
		public IActionResult Index()
		{
			


			List<Product> products = _unitOfWork.Product.GetAll().ToList();
			return View(products);
		}
		[HttpPost]
		public IActionResult Add(Product product)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.Product.Add(product);
				_unitOfWork.Save();
				TempData["Success"] = "Product added successfuly";
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
			Product product = _unitOfWork.Product.Get(u => u.Id == id);
			if (product == null)
			{
				return NotFound();
			}
			else
			{
				return View(product);
			}
		}
		[HttpPost]
		public IActionResult Edit(Product product)
		{

			if (ModelState.IsValid)
			{
				_unitOfWork.Product.Update(product);
				_unitOfWork.Save();
				TempData["Success"] = "Edit is successful";
				return RedirectToAction("Index");
			}
			return View(product);
		}
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{

				return NotFound();
			}
			Product product = _unitOfWork.Product.Get(u => u.Id == id);
			if (product == null)
			{
				return NotFound();
			}
			else
			{
				return View(product);
			}

		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			Product product = _unitOfWork.Product.Get(u => u.Id == id);
			if (product != null)
			{
				_unitOfWork.Product.Remove(product);
				_unitOfWork.Save();
				TempData["Success"] = "Delete is successful";

			}
			return RedirectToAction("Index");
		}
	}
}
