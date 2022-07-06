using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Dto
{
    public class LikedPostDto
    {
        public int PostId { get; set; }
        public IEnumerable<UserWhoLikedDto> UsersWhoLiked { get; set; }

    }
}
