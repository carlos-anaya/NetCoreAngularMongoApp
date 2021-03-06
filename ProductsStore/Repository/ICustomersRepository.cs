﻿using ProductsStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsStore.Repository
{
    public interface ICustomersRepository
    {
        Task<List<Customer>> GetCustomersAsync();
        Task<PagedResult<Customer>> GetCustomersPagedAsync(int skip, int take);
        Task<Customer> GetCustomerAsync(string id);
        Task<Customer> InsertCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(Customer customer);
    }
}