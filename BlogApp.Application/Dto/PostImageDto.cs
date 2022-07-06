using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Dto
{
    public class PostImageDto : Dto
    {
        public string Source { get; set; }
        public int PostId { get; set; }
    }
}
