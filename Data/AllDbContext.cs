using Microsoft.EntityFrameworkCore;
using OnlineShopping.Models;

namespace OnlineShopping.Data
{
    public class AllDbContext : DbContext   
    {
        public AllDbContext(DbContextOptions<AllDbContext> options) :base(options){ }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Product> Products { get; set; }
        
        public DbSet<Customer> Customers { get; set; }


    }
}
