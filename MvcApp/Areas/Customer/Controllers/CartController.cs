using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize]
	public class CartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private ShoppingCardVM ShoppingCardVM { set; get; }
		public CartController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;

		}
		public IActionResult Index()
		{
			var claimsIdentiy = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentiy.FindFirst(ClaimTypes.NameIdentifier).Value;
			ShoppingCardVM = new ShoppingCardVM()
			{
				ShoppingCardList = _unitOfWork.ShoppingCard.GetAll(u => u.ApplicationUserId == userId,
				includeProperties: "Product"),

			};
			foreach (ShoppingCard shoppingCard in ShoppingCardVM.ShoppingCardList)
			{
				shoppingCard.Price = GetPriceBasedOnCount(shoppingCard);
				ShoppingCardVM.Total += shoppingCard.Price * shoppingCard.Count;
			}


			return View(ShoppingCardVM);
		}
		public IActionResult Summary()
		{
			return View();
		}
		public IActionResult Plus(int cardId)
		{
			ShoppingCard card = _unitOfWork.ShoppingCard.Get(u => u.Id == cardId);
			card.Count++;
			_unitOfWork.ShoppingCard.Update(card);
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));

		}
		public IActionResult Minus(int cardId)
		{
			ShoppingCard card = _unitOfWork.ShoppingCard.Get(u => u.Id == cardId);
			if (card.Count <= 1)
			{
				_unitOfWork.ShoppingCard.Remove(card);
			}
			else
			{
				card.Count--;
				_unitOfWork.ShoppingCard.Update(card);
			}

			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));

		}
		public IActionResult Remove(int cardId)
		{
			ShoppingCard card = _unitOfWork.ShoppingCard.Get(u => u.Id == cardId);
			_unitOfWork.ShoppingCard.Remove(card);
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));

		}
		
		private double GetPriceBasedOnCount(ShoppingCard shoppingCard)
		{
			if (shoppingCard.Count <= 50)
			{
				return shoppingCard.Product.Price;
			}
			else if (shoppingCard.Count <= 100)
			{
				return shoppingCard.Product.Price50;
			}
			else
			{
				return shoppingCard.Product.Price100;
			}
		}
	}
}
