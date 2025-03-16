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
			//_dbcontext.Products.Update(product);
			Product obj = _dbcontext.Products.SingleOrDefault(u => u.Id == product.Id);
			if (obj != null)
			{
				obj.ISBN = product.ISBN;
				obj.ListPrice = product.ListPrice;
				obj.Price = product.Price;
				obj.Price50 = product.Price50;
				obj.Price100 = product.Price100;
				obj.Category = product.Category;
				obj.Description = product.Description;
				obj.Author = product.Author;
				obj.CategoryId = product.CategoryId;
				obj.Id = product.Id;
				obj.Title = product.Title;
				if (product.ImageUrl != null)
				{
					obj.ImageUrl = product.ImageUrl;
				}

			}

		}
	}
}
