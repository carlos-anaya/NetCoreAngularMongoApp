using ProductsStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsStore.Repository
{
    public interface IStatesRepository
    {
        Task<List<State>> GetStatesAsync();
    }
}