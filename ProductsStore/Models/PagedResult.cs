using System.Collections.Generic;

namespace ProductsStore.Models
{
    public struct PagedResult<T>
    {
        public int TotalRecords { get; set; }
        public IEnumerable<T> Records { get; set; }

        public PagedResult(int totalRecords, IEnumerable<T> records)
        {
            TotalRecords = totalRecords;
            Records = records;
        }
    }
}