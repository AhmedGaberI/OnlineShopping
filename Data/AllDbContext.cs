using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Models;

namespace OnlineShopping.Data
{
    public class AllDbContext : IdentityDbContext    
    {
       
        public DbSet<Company> Companies { get; set; }
        public DbSet<Product> Products { get; set; }
        
        public DbSet<Customer> Customers { get; set; }

        public AllDbContext(DbContextOptions<AllDbContext> options) : base(options) { }


    }
}
