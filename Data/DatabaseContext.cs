using EcommerceApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EcommerceApplication.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
