using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ProductsStore.Models
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        public Customer Customer { get; set; }
        public ModelStateDictionary ModelStateDictionary { get; set; }
    }
}