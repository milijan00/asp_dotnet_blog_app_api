﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain
{
    public class LikedPosts  
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public virtual User User { get; set; } 
        public virtual Post Post { get; set; }

    }
}
