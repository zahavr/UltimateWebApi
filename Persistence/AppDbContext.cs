using Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations;
using System.Reflection;

namespace Persistence
{
	public class AppDbContext : DbContext
	{
		public DbSet<Company> Companies { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public AppDbContext(DbContextOptions options) : base(options)
		{
	
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new CompanyConfiguration());
			modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
		}
	}
}
