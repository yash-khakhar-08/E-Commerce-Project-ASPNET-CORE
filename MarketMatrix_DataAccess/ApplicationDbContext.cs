using MarketMatrix_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarketMatrix_DataAccess
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		//to create table
		public DbSet<Category> Categories { get; set; }
		public DbSet<Products> Products { get; set; }
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<MyCart> MyCart { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Orders> Orders { get; set; }

        // to insert data command line 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 1, Name = "T-shirts", SectionName = "Male" },
				new Category { Id = 2, Name = "Shirts", SectionName = "Male" }
				);


            modelBuilder.Entity<Products>()
				.HasOne(p => p.Category)
				.WithMany(c => c.Products)
				.HasForeignKey(p => p.CategoryId);
        }
	}
}
