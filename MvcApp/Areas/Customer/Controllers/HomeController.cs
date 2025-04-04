using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitOfWork;
		public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
			return View(productList);
		}
		public IActionResult Details(int productId)
		{
			ShoppingCard shoppingCard = new ShoppingCard()
			{
				Product = _unitOfWork.Product.Get(u => u.Id == productId, includeProperties: "Category"),
				ProductId = productId,
				Count = 1

			};

			return View(shoppingCard);
		}
		[HttpPost]
		[Authorize]
		public IActionResult Details(ShoppingCard shoppingCard)
		{
			var claimsIdentiy = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentiy.FindFirst(ClaimTypes.NameIdentifier).Value;
			shoppingCard.ApplicationUserId = userId;
			ShoppingCard existShoppingCard = _unitOfWork.ShoppingCard.Get(u => u.ApplicationUserId == userId
			&& u.ProductId == shoppingCard.ProductId);
			if (existShoppingCard != null)
			{
				existShoppingCard.Count += shoppingCard.Count;
				_unitOfWork.ShoppingCard.Update(existShoppingCard);
			}
			else
			{
				_unitOfWork.ShoppingCard.Add(shoppingCard);
			}
			TempData["success"] = "Card updated successfully";
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}
		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
