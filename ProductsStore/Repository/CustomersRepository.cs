using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductsStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsStore.Repository
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger _logger;

        public CustomersRepository(AppDbContext appDbContext, ILoggerFactory loggerFactory)
        {
            _appDbContext = appDbContext;
            _logger = loggerFactory.CreateLogger(nameof(CustomersRepository));
        }
        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _appDbContext.Customers
                .Include(c => c.State)
                .ToListAsync();
        }
    }
}
