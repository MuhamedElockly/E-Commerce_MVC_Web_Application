using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

using BulkyBook.Models.Data;
namespace BulkyBook.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _context;
		private DbSet<T> _dbSet;
		public Repository(ApplicationDbContext dbContext)
		{
			_context = dbContext;
			_dbSet = _context.Set<T>();
			//	_context.Products.Include(u => u.Category).Include(u => u.Category.Id);

		}
		public void Add(T entity)
		{
			_dbSet.Add(entity);

		}

		public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
		{
			IQueryable<T> query;
			if (tracked)
			{
				query = _dbSet;
			}
			else
			{
				query = _dbSet.AsNoTracking();
			}
			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var property in includeProperties
					.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(property);
				}
			}
			query = query.Where(filter);
			return query.FirstOrDefault();
		}

		public IEnumerable<T> GetAll(string? includeProperties = null)
		{
			IQueryable<T> query = _dbSet;
			if (!string.IsNullOrEmpty(includeProperties))
			{
				foreach (var property in includeProperties
					.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{

					query = query.Include(property);
				}
			}

			return query.ToList();
		}

		public void Remove(T entity)
		{
			_dbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entities)
		{
			_dbSet.RemoveRange(entities);
		}

	}
}
