using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Collections.Generic;

namespace ProductsStore.Models
{
    public class Customer
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public int Zip { get; set; }
        public string Gender { get; set; }
        public int OrderCount { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}