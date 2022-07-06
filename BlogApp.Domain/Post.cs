using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain
{
    public class Post : Entity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public virtual Category Category { get; set; }
        public virtual User Author { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<PostsImage> PostsImages { get; set; } = new List<PostsImage>();
        public virtual ICollection<LikedPosts> UsersWhoLiked { get; set;  } = new List<LikedPosts>();
    }
}
