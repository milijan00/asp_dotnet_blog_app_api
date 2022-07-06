using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain
{
    public class PostsImage :Entity 
    {
        public int PostId { get; set; }
        public virtual Post Post { get; set; } 
        public string Source { get; set; }

    }
}
