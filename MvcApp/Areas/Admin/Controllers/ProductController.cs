using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModel;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	//[Authorize(Roles = SD.Role_Admin)]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
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

			List<Product> products = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
			return View(products);
		}
		[HttpPost]
		public IActionResult Upsert(ProductVM productVM, IFormFile? formFile)
		{
			if (ModelState.IsValid)
			{
				string wwwRootPath = _webHostEnvironment.WebRootPath;
				if (formFile != null)
				{
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
					string productPath = Path.Combine(wwwRootPath, @"images\product");


					if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
					{
						var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
						if (System.IO.File.Exists(oldImagePath))
						{
							System.IO.File.Delete(oldImagePath);
						}
					}


					using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
					{
						formFile.CopyTo(fileStream);

					}
					productVM.Product.ImageUrl = @"\images\product\" + fileName;
				}

				if (productVM.Product.Id == 0)
				{
					_unitOfWork.Product.Add(productVM.Product);
				}
				else
				{
					_unitOfWork.Product.Update(productVM.Product);
				}

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


		[HttpGet]
		public IActionResult GetAll()
		{
			List<Product> products = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
			return Json(new { data = products });
		}

		[HttpDelete]
		public IActionResult Delete(int? id)
		{
			Product productToBeDelete = _unitOfWork.Product.Get(u => u.Id == id);
			if (productToBeDelete == null)
			{
				return Json(new { success = false, message = "error while deleting" });
			}
			else
			{
				if (!string.IsNullOrEmpty(productToBeDelete.ImageUrl))
				{
					var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDelete.ImageUrl.TrimStart('\\'));
					if (System.IO.File.Exists(oldImagePath))
					{
						System.IO.File.Delete(oldImagePath);
					}
				}
				_unitOfWork.Product.Remove(productToBeDelete);
				_unitOfWork.Save();
				return Json(new { success = true, message = "Delete successful" });
			}
		}

	}
}
