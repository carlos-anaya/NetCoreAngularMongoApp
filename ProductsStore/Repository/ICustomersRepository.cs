using ProductsStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsStore.Repository
{
    public interface ICustomersRepository
    {
        Task<List<Customer>> GetCustomersAsync();
    }
}
