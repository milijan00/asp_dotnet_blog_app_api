using BlogApp.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.UseCases.Queries
{
    public  interface IGetPostsLikesQuery : IQuery<int, LikedPostDto>
    {
    }
}
