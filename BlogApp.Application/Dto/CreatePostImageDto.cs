using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Dto
{
    public class CreatePostImageDto
    {
        public int? PostId { get; set; }
        public IEnumerable<string> Images { get; set; }
    }
}
