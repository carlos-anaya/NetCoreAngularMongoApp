using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                .Include(c => c.Orders)
                .ToListAsync();
        }

        public async Task<PagedResult<Customer>> GetCustomersPagedAsync(int skip, int take)
        {
            return new PagedResult<Customer>
            {
                TotalRecords = await _appDbContext.Customers.CountAsync(),
                Records = await _appDbContext.Customers
                    .OrderBy(c => c.LastName)
                    .Include(c => c.State)
                    .Include(c => c.Orders)
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync()
            };
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            return await _appDbContext.Customers
                .Include(c => c.State)
                .Include(c => c.Orders)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer> InsertCustomerAsync(Customer customer)
        {
            _appDbContext.Add(customer);
            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(InsertCustomerAsync)}. Exception:\n {e.Message}");
                throw;
            }
            return customer;
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            _appDbContext.Customers.Attach(customer);
            _appDbContext.Entry(customer).State = EntityState.Modified;

            try
            {
                return await _appDbContext.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(UpdateCustomerAsync)}. Exception:\n {e.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteCustomerAsync(Customer customer)
        {
            _appDbContext.Remove(customer);

            try
            {
                return await _appDbContext.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(DeleteCustomerAsync)}. Exception:\n " + e.Message);
                return false;
            }
        }
    }
}