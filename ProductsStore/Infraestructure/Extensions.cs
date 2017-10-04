using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace ProductsStore.Infraestructure
{
    public static class Extensions
    {
        public static Dictionary<string, IEnumerable<string>> GetErrors(this ModelStateDictionary modelState)
        {
            return modelState
                .Select(p => new { key = p.Key, errors = p.Value.Errors.Select(e => e.ErrorMessage) })
                .ToDictionary(kv => kv.key, kv => kv.errors);
        }
    }
}