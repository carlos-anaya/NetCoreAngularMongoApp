using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using ProductsStore.Models;
using System;
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
            return await _appDbContext.Customers.Find(_ => true).ToListAsync();
        }

        public async Task<PagedResult<Customer>> GetCustomersPagedAsync(int skip, int take)
        {
            var count = await _appDbContext.Customers.CountAsync(_ => true);
            return new PagedResult<Customer>
            {
                TotalRecords = int.Parse(count.ToString()),
                Records = await _appDbContext.Customers.Find(_ => true)
                    .Sort("{LastName: 1}")
                    .Skip(skip)
                    .Limit(take)
                    .ToListAsync()
            };
        }

        public async Task<Customer> GetCustomerAsync(string id)
        {
            var filter = Builders<Customer>.Filter.Eq(c => c.Id, id);
            return await _appDbContext.Customers.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<Customer> InsertCustomerAsync(Customer customer)
        {
            customer.Id = ObjectId.GenerateNewId().ToString();
            try
            {
                await _appDbContext.Customers.InsertOneAsync(customer);
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
            var filter = Builders<Customer>.Filter.Eq(c => c.Id, customer.Id);

            try
            {
                await _appDbContext.Customers.ReplaceOneAsync(filter, customer);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(UpdateCustomerAsync)}. Exception:\n {e.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteCustomerAsync(Customer customer)
        {
            var filter = Builders<Customer>.Filter.Eq(c => c.Id, customer.Id);

            try
            {
                await _appDbContext.Customers.DeleteOneAsync(filter);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(DeleteCustomerAsync)}. Exception:\n " + e.Message);
                return false;
            }
        }
    }
}