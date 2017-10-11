using MongoDB.Driver;
using ProductsStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsStore.Repository
{
    public class StatesRepository : IStatesRepository
    {
        private readonly AppDbContext _appDbContext;

        public StatesRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<State>> GetStatesAsync()
        {
            return await _appDbContext.States.Find(_ => true).Sort("{Name: 1}").ToListAsync();
        }
    }
}