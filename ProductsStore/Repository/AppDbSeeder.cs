using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using ProductsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace ProductsStore.Repository
{
    public class AppDbSeeder
    {
        private readonly ILogger _logger;

        public AppDbSeeder(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(nameof(AppDbSeeder));
        }

        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            //Based on EF team's example at https://github.com/aspnet/MusicStore/blob/dev/samples/MusicStore/Models/SampleData.cs
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var customersDb = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (await customersDb.Customers.CountAsync(_ => true) <= 0)
                {
                    await InsertCustomersSampleData(customersDb);
                }

            }
        }

        public async Task InsertCustomersSampleData(AppDbContext db)
        {
            var states = GetStates();

            try
            {
                db.States.InsertMany(states);
                var numAffected = await db.States.CountAsync(_ => true);
                _logger.LogInformation($"Saved {numAffected} states");
            }
            catch (Exception exp)
            {
                _logger.LogError($"Error in {nameof(AppDbSeeder)}: " + exp.Message);
                throw;
            }

            var customers = GetCustomers(states);

            try
            {
                db.Customers.InsertMany(customers);
                var numAffected = await db.States.CountAsync(_ => true);
                _logger.LogInformation($"Saved {numAffected} customers");
            }
            catch (Exception exp)
            {
                _logger.LogError($"Error in {nameof(AppDbSeeder)}: " + exp.Message);
                throw;
            }

        }

        private static IEnumerable<Customer> GetCustomers(IReadOnlyCollection<State> states)
        {
            //Customers
            var customerNames = new[]
            {
                "Marcus,HighTower,Male,acmecorp.com",
                "Jesse,Smith,Female,gmail.com",
                "Albert,Einstein,Male,outlook.com",
                "Dan,Wahlin,Male,yahoo.com",
                "Ward,Bell,Male,gmail.com",
                "Brad,Green,Male,gmail.com",
                "Igor,Minar,Male,gmail.com",
                "Miško,Hevery,Male,gmail.com",
                "Michelle,Avery,Female,acmecorp.com",
                "Heedy,Wahlin,Female,hotmail.com",
                "Thomas,Martin,Male,outlook.com",
                "Jean,Martin,Female,outlook.com",
                "Robin,Cleark,Female,acmecorp.com",
                "Juan,Paulo,Male,yahoo.com",
                "Gene,Thomas,Male,gmail.com",
                "Pinal,Dave,Male,gmail.com",
                "Fred,Roberts,Male,outlook.com",
                "Tina,Roberts,Female,outlook.com",
                "Cindy,Jamison,Female,gmail.com",
                "Robyn,Flores,Female,yahoo.com",
                "Jeff,Wahlin,Male,gmail.com",
                "Danny,Wahlin,Male,gmail.com",
                "Elaine,Jones,Female,yahoo.com",
                "John,Papa,Male,gmail.com"
            };
            var addresses = new[]
            {
                "1234 Anywhere St.",
                "435 Main St.",
                "1 Atomic St.",
                "85 Cedar Dr.",
                "12 Ocean View St.",
                "1600 Amphitheatre Parkway",
                "1604 Amphitheatre Parkway",
                "1607 Amphitheatre Parkway",
                "346 Cedar Ave.",
                "4576 Main St.",
                "964 Point St.",
                "98756 Center St.",
                "35632 Richmond Circle Apt B",
                "2352 Angular Way",
                "23566 Directive Pl.",
                "235235 Yaz Blvd.",
                "7656 Crescent St.",
                "76543 Moon Ave.",
                "84533 Hardrock St.",
                "5687534 Jefferson Way",
                "346346 Blue Pl.",
                "23423 Adams St.",
                "633 Main St.",
                "899 Mickey Way"
            };

            var citiesStates = new[]
            {
                "Phoenix,AZ,Arizona",
                "Encinitas,CA,California",
                "Seattle,WA,Washington",
                "Chandler,AZ,Arizona",
                "Dallas,TX,Texas",
                "Orlando,FL,Florida",
                "Carey,NC,North Carolina",
                "Anaheim,CA,California",
                "Dallas,TX,Texas",
                "New York,NY,New York",
                "White Plains,NY,New York",
                "Las Vegas,NV,Nevada",
                "Los Angeles,CA,California",
                "Portland,OR,Oregon",
                "Seattle,WA,Washington",
                "Houston,TX,Texas",
                "Chicago,IL,Illinois",
                "Atlanta,GA,Georgia",
                "Chandler,AZ,Arizona",
                "Buffalo,NY,New York",
                "Albuquerque,AZ,Arizona",
                "Boise,ID,Idaho",
                "Salt Lake City,UT,Utah",
                "Orlando,FL,Florida"
            };

            const int zip = 85229;

            var orders = new List<Order>
            {
                new Order { Id = ObjectId.GenerateNewId().ToString(), Product = "Basket", Price = 29.99M, Quantity = 1 },
                new Order {Id = ObjectId.GenerateNewId().ToString(), Product = "Yarn", Price = 9.99M, Quantity = 1 },
                new Order { Id = ObjectId.GenerateNewId().ToString(), Product = "Needes", Price = 5.99M, Quantity = 1 },
                new Order { Id = ObjectId.GenerateNewId().ToString(), Product = "Speakers", Price = 499.99M, Quantity = 1 },
                new Order { Id = ObjectId.GenerateNewId().ToString(), Product = "iPod", Price = 399.99M, Quantity = 1 },
                new Order { Id = ObjectId.GenerateNewId().ToString(), Product = "Table", Price = 329.99M, Quantity = 1 },
                new Order { Id = ObjectId.GenerateNewId().ToString(), Product = "Chair", Price = 129.99M, Quantity = 4 },
                new Order { Id = ObjectId.GenerateNewId().ToString(), Product = "Lamp", Price = 89.99M, Quantity = 5 },
                new Order { Id = ObjectId.GenerateNewId().ToString(), Product = "Call of Duty", Price = 59.99M, Quantity = 1 },
                new Order { Id = ObjectId.GenerateNewId().ToString(), Product = "Controller", Price = 49.99M, Quantity = 1 },
                new Order { Id = ObjectId.GenerateNewId().ToString(), Product = "Gears of War", Price = 49.99M, Quantity = 1 },
                new Order { Id = ObjectId.GenerateNewId().ToString(), Product = "Lego City", Price = 49.99M, Quantity = 1 },
                new Order { Id = ObjectId.GenerateNewId().ToString(), Product = "Baseball", Price = 9.99M, Quantity = 5 },
                new Order { Id = ObjectId.GenerateNewId().ToString(), Product = "Bat", Price = 19.99M, Quantity = 1 }
            };

            var ordersLength = orders.Count;
            var customers = new List<Customer>();
            var random = new Random();

            for (var i = 0; i < customerNames.Length; i++)
            {
                var nameGenderHost = customerNames[i].Split(',');
                var cityState = citiesStates[i].Split(',');
                var state = states.SingleOrDefault(s => s.Abbreviation == cityState[1]);

                var customer = new Customer
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    FirstName = nameGenderHost[0],
                    LastName = nameGenderHost[1],
                    Email = nameGenderHost[0] + '.' + nameGenderHost[1] + '@' + nameGenderHost[3],
                    Address = addresses[i],
                    City = cityState[0],
                    State = state,
                    Zip = zip + i,
                    Gender = nameGenderHost[2],
                    OrderCount = 0
                };

                var firstOrder = (int)Math.Floor(random.NextDouble() * orders.Count);
                var lastOrder = (int)Math.Floor(random.NextDouble() * orders.Count);

                if (firstOrder > lastOrder)
                {
                    var tempOrder = firstOrder;
                    firstOrder = lastOrder;
                    lastOrder = tempOrder;
                }

                customer.Orders = new List<Order>();

                for (var j = firstOrder; j <= lastOrder && j < ordersLength; j++)
                {
                    var order = new Order
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        Product = orders[j].Product,
                        Price = orders[j].Price,
                        Quantity = orders[j].Quantity
                    };
                    customer.Orders.Add(order);
                }
                customer.OrderCount = customer.Orders.Count;
                customers.Add(customer);
            }

            return customers;
        }

        private static List<State> GetStates()
        {
            var states = new List<State>
            {
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Alabama", Abbreviation = "AL" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Montana", Abbreviation = "MT" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Alaska", Abbreviation = "AK" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Nebraska", Abbreviation = "NE" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Arizona", Abbreviation = "AZ" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Nevada", Abbreviation = "NV" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Arkansas", Abbreviation = "AR" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "New Hampshire", Abbreviation = "NH" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "California", Abbreviation = "CA" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "New Jersey", Abbreviation = "NJ" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Colorado", Abbreviation = "CO" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "New Mexico", Abbreviation = "NM" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Connecticut", Abbreviation = "CT" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "New York", Abbreviation = "NY" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Delaware", Abbreviation = "DE" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "North Carolina", Abbreviation = "NC" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Florida", Abbreviation = "FL" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "North Dakota", Abbreviation = "ND" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Georgia", Abbreviation = "GA" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Ohio", Abbreviation = "OH" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Hawaii", Abbreviation = "HI" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Oklahoma", Abbreviation = "OK" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Idaho", Abbreviation = "ID" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Oregon", Abbreviation = "OR" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Illinois", Abbreviation = "IL" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Pennsylvania", Abbreviation = "PA" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Indiana", Abbreviation = "IN" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Rhode Island", Abbreviation = "RI" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Iowa", Abbreviation = "IA" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "South Carolina", Abbreviation = "SC" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Kansas", Abbreviation = "KS" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "South Dakota", Abbreviation = "SD" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Kentucky", Abbreviation = "KY" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Tennessee", Abbreviation = "TN" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Louisiana", Abbreviation = "LA" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Texas", Abbreviation = "TX" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Maine", Abbreviation = "ME" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Utah", Abbreviation = "UT" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Maryland", Abbreviation = "MD" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Vermont", Abbreviation = "VT" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Massachusetts", Abbreviation = "MA" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Virginia", Abbreviation = "VA" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Michigan", Abbreviation = "MI" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Washington", Abbreviation = "WA" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Minnesota", Abbreviation = "MN" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "West Virginia", Abbreviation = "WV" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Mississippi", Abbreviation = "MS" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Wisconsin", Abbreviation = "WI" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Missouri", Abbreviation = "MO" },
                new State {  Id = ObjectId.GenerateNewId().ToString(), Name = "Wyoming", Abbreviation = "WY" }
            };

            return states;
        }
    }
}