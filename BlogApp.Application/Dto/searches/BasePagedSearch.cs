using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Dto.searches
{
    public class BasePagedSearch
    {
        public int? Page { get; set; } = 1;
        public int? PerPage { get; set; } = 20;
    }
}
