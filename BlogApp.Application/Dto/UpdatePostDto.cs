using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Dto
{
    public class UpdatePostDto : Dto
    {
        public int? CategoryId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
