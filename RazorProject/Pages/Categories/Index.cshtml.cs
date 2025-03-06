using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorProject.Data;
using RazorProject.Models;

namespace RazorProject.Categories
{
    public class IndexModel : PageModel

    {
        private readonly ApplicationDbContext _dbContext;
        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        public List<Category> categories { set; get; }
        public void OnGet()
        {
            categories=_dbContext.Categories.ToList();
        }
    }
}
