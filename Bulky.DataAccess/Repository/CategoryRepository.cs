using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using MvcApp.Bulky.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public void Save()
        {
          _dbContext.SaveChanges();
        }

        public void Update(Category category)
        {
           _dbContext.Update(category);
        }
    }
}
