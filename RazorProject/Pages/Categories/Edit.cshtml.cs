using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorProject.Data;
using RazorProject.Models;

namespace RazorProject.Pages.Categories
{
	[BindProperties]
	public class EditModel : PageModel
	{
		private readonly ApplicationDbContext _context;
		public Category Category { get; set; }
		public EditModel(ApplicationDbContext dbContext)
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
		public IActionResult OnPost(Category category)
		{
			if (ModelState.IsValid)
			{
				_context.Categories.Update(category);
				_context.SaveChanges();
				TempData["success"] = "Edit is successful";
				return RedirectToPage("Index");
			}
			return Page();

		}
	}
}
