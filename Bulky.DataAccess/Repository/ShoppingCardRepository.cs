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
	public class ShoppingCardRepository : Repository<ShoppingCard>, IShoppingCardRepository
	{
		private readonly ApplicationDbContext _context;
		public ShoppingCardRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
			_context = dbContext;
		}

		public void Update(ShoppingCard shoppingCard)
		{
			_context.ShoppingCards.Update(shoppingCard);
		}
	}
}
