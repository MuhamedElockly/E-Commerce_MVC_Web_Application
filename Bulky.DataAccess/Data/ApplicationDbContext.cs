using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.Models.Data
{
    public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<Category> Categories { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 1, Name = "Mobil", DisplayOrder = 1 },
				new Category { Id = 2, Name = "Car", DisplayOrder = 2 },
				new Category { Id = 3, Name = "Labtop", DisplayOrder = 3 },
				new Category { Id = 4, Name = "Tv", DisplayOrder = 4 }
				);
		}
	}
}
