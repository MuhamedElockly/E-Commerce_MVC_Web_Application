using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorProject.Data;
using RazorProject.Models;

namespace RazorProject.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        public CreateModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public Category Category { get; set; }
        public void OnGet()
        {
           
        }
        public IActionResult OnPost(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
			TempData["success"] = "Create is successful";
			return RedirectToPage("Index");

        }
    }
}
