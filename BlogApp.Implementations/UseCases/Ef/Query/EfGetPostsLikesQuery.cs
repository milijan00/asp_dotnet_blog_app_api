using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Queries;
using BlogApp.DataAccess;
using BlogApp.Implementations.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Query
{
    public class EfGetPostsLikesQuery : EfBase, IGetPostsLikesQuery
    {
        public EfGetPostsLikesQuery(BlogAppDbContext context) : base(context)
        {
        }

        public int Id => 27;

        public string Name => "EfGetPostsLikes";

        public string Description => "Get posts likes  via entity framework.";

        public LikedPostDto Execute(int id)
        {
            // treba da dohvatim i podatke o korisnicima
            //var likedPost = this.context.LikedPosts.Where(lp => lp.PostId == id).Include(lp => lp.User);
            //if(likedPost)
            var post = this.context.Posts.Include(p => p.UsersWhoLiked).ThenInclude(lp => lp.User).FirstOrDefault(p => p.Id == id);
            if(post == null)
            {
                throw new EntityNotFoundException("Post", id);
            }

            return new LikedPostDto
            {
                PostId = id,
                UsersWhoLiked = post.UsersWhoLiked.Select(pl => new UserWhoLikedDto
                {
                    Username = pl.User.Username,
                    ImageSource = pl.User.ProfilePicture,
                    UserId = pl.User.Id
                }).ToList()
            };

        }
    }
}
