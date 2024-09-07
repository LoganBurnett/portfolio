using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

// Responsible for communicating with the database
namespace DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }  //the physical DB table will be name Categories

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<ShoppingCart> ShoppingCart { get; set; }

        public DbSet<OrderHeader> OrderHeader { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }
	}
}
