using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductsStore.Models;
using System;

namespace ProductsStore.Repository
{
    public class AppDbContext : DbContext
    {
        private readonly IMongoDatabase _database;

        public AppDbContext(IOptions<MongoSettings> settings, ILoggerFactory logger)
        {
            try
            {
                var client = new MongoClient(settings.Value.ConnectionString);
                _database = client.GetDatabase(settings.Value.Database);
            }
            catch (Exception e)
            {
                logger.CreateLogger(nameof(AppDbContext)).LogError(e.Message);
            }
        }

        public IMongoCollection<Customer> Customers => _database.GetCollection<Customer>("Customer");
        public IMongoCollection<Order> Orders => _database.GetCollection<Order>("Order");
        public IMongoCollection<State> States => _database.GetCollection<State>("State");
    }
}