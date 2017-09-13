using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductsStore.Models;

namespace ProductsStore.Repository
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<State> States { get; set; }

        public AppDbContext(IConfiguration configuration, DbContextOptions options) : base(options)
        {
            _configuration = configuration;
        }
    }
}