using System.Collections.Generic;

namespace ProductsStore.Models
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        public Customer Customer { get; set; }
        public Dictionary<string, IEnumerable<string>> ModelStateDictionary { get; set; }
    }
}