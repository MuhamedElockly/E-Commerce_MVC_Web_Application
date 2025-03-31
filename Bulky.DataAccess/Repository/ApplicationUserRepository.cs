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
	public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
	{
		private readonly ApplicationDbContext _context;
		public ApplicationUserRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
			_context = dbContext;
		}

		public void Update(ApplicationUser applicationUser)
		{
			_context.ApplicationUsers.Add(applicationUser);
		}
	}
}
