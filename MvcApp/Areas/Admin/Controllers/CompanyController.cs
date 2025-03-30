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
	public class CompanyController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		
		public CompanyController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			
		}
		public IActionResult Upsert(int? id)
		{
			
			

			if (id == null || id == 0)
			{
				return View(new Company());
			}
			else
			{
				Company company = _unitOfWork.Company.Get(u => u.Id == id);
				return View(company);
			}

		}
		public IActionResult Index()
		{

			List<Company> Companys = _unitOfWork.Company.GetAll().ToList();
			return View(Companys);
		}
		[HttpPost]
		public IActionResult Upsert(Company Company)
		{
			if (ModelState.IsValid)
			{
				

				if (Company.Id == 0)
				{
					_unitOfWork.Company.Add(Company);
				}
				else
				{
					_unitOfWork.Company.Update(Company);
				}

				_unitOfWork.Save();
				TempData["Success"] = "Company added successfuly";
				return RedirectToAction("Index");
			}
			return View(Company);

		}


		[HttpGet]
		public IActionResult GetAll()
		{
			List<Company> Companys = _unitOfWork.Company.GetAll().ToList();
			return Json(new { data = Companys });
		}

		[HttpDelete]
		public IActionResult Delete(int? id)
		{
			Company CompanyToBeDelete = _unitOfWork.Company.Get(u => u.Id == id);
			if (CompanyToBeDelete == null)
			{
				return Json(new { success = false, message = "error while deleting" });
			}
			else
			{
				
				_unitOfWork.Company.Remove(CompanyToBeDelete);
				_unitOfWork.Save();
				return Json(new { success = true, message = "Delete successful" });
			}
		}

	}
}
