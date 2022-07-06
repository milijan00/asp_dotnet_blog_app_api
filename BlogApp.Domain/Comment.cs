using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain
{
    public class Comment : Entity
    {
        public string Content { get; set; }

        public virtual Post Post { get; set; }

        public virtual Comment Parent { get; set; }
        public virtual ICollection<Comment> ChildComments { get; set; } = new List<Comment>();
        public int UserId { get; set; }
        public int? ParentId { get; set; }
        public int PostId { get; set; }

        public virtual User CommentedBy { get; set; } 
        //public virtual ICollection<CommentsLike> UsersWhoLiked { get; set; } = new List<CommentsLike>();
        public virtual ICollection<LikedComment> UsersWhoLiked { get; set; } = new List<LikedComment>();
    }
}
