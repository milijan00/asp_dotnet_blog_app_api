using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Dto
{
    public class LikedCommentDto 
    {
        public ICollection<UserWhoLikedDto> usersWhoLiked { get; set; } = new List<UserWhoLikedDto>();
        public int CommentId { get; set; }
    }
    }
