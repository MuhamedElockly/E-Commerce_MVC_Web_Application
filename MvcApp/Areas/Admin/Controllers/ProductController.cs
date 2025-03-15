using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModel;
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
		public IActionResult Upsert(int? id)
		{
			IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll()
				.Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()

				});

			ProductVM productVM = new ProductVM()
			{

				CategoryList = CategoryList,
				Product = new Product()
			};

			if (id == null || id == 0)
			{
				return View(productVM);
			}
			else
			{
				productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
				return View(productVM);
			}

		}
		public IActionResult Index()
		{



			List<Product> products = _unitOfWork.Product.GetAll().ToList();
			return View(products);
		}
		[HttpPost]
		public IActionResult Upsert(ProductVM productVM, IFormFile? formFile)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.Product.Add(productVM.Product);
				_unitOfWork.Save();
				TempData["Success"] = "Product added successfuly";
				return RedirectToAction("Index");
			}
			else
			{
				productVM.CategoryList = _unitOfWork.Category.GetAll()
						.Select(u => new SelectListItem

						{
							Text = u.Name,
							Value = u.Id.ToString()
						}
							);

				return View(productVM);
			}

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
