using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
	public class ProductRepsitory : Repository<Product>, IProductRepository
	{
		private readonly ApplicationDbContext _dbcontext;
		public ProductRepsitory(ApplicationDbContext dbContext) : base(dbContext)
		{
			_dbcontext = dbContext;
		}

		public void Update(Product product)
		{
			_dbcontext.Products.Update(product);
		}
	}
}
