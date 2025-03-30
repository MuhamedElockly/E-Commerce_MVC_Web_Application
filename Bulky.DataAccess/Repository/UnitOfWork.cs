using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		ApplicationDbContext _dbcontext;
		public ICategoryRepository Category { get; private set; }
		public IProductRepository Product { get; private set; }

		public ICompanyRepository Company {get; private set; }

		public UnitOfWork(ApplicationDbContext dbContext)
		{
			_dbcontext = dbContext;
			Category = new CategoryRepository(_dbcontext);
			Product = new ProductRepsitory(_dbcontext);
			Company = new CompanyRepository(_dbcontext);	
		}


		public void Save()
		{
			_dbcontext.SaveChanges();

		}
	}
}
