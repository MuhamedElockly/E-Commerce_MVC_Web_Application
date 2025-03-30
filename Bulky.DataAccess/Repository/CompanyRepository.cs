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
	internal class CompanyRepository : Repository<Company>, ICompanyRepository
	{
		private readonly ApplicationDbContext _dbContext;
		public CompanyRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public void Update(Company company)
		{
			_dbContext.Companies.Update(company);
		}
	}
}
