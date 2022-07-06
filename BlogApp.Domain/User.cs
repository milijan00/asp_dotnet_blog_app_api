using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain
{
    public class User : Entity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public virtual List<Post> CreatedPosts { get; set; } = new List<Post>();
        public virtual Role Role { get; set; } 
        public int RoleId { get; set; }
        public string ProfilePicture { get; set; }

        public virtual ICollection<LikedComment> LikedComments { get; set; } = new List<LikedComment>();
        public virtual ICollection<LikedPosts> LikedPosts { get; set; } = new List<LikedPosts>();
        public virtual ICollection<Comment> CreatedComments { get; set; } = new List<Comment>();
        public virtual ICollection<UserUseCase> UseCases { get; set; } = new List<UserUseCase>();


    }
}
