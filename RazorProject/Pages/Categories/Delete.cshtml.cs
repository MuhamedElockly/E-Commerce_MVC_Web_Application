using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorProject.Data;
using RazorProject.Models;

namespace RazorProject.Pages.Categories
{
	[BindProperties]
	public class DeleteModel : PageModel
	{
		private readonly ApplicationDbContext _context;
		public Category Category { get; set; }
		public DeleteModel(ApplicationDbContext dbContext)
		{
			_context = dbContext;
		}
		public void OnGet(int? id)
		{
			if (id != null && id != 0)
			{
				Category = _context.Categories.FirstOrDefault(c => c.Id == id);
			}
		}
		public IActionResult OnPost(int id)
		{

			_context.Categories.Remove(_context.Categories.Find(id));
			_context.SaveChanges();
			TempData["success"] = "Delete is successful";
			return RedirectToPage("Index");
		}
	}
}
