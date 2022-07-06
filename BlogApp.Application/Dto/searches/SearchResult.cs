using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Dto.searches
{
    public class SearchResult<T> where T : class
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalCount { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
    }
}
