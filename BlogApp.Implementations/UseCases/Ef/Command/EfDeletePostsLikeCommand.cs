using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Commands;
using BlogApp.DataAccess;
using BlogApp.DataAccess.Extensions;
using BlogApp.Implementations.Exceptions;
using BlogApp.Implementations.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Command
{
    public class EfDeletePostsLikeCommand : EfBase, IDeletePostsLikeCommand
    {
        private DeletePostsLikeValidator _validator;
        public EfDeletePostsLikeCommand(BlogAppDbContext context, DeletePostsLikeValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 29;

        public string Name => "EfDeletePostsLike";

        public string Description => "Remove a like from a post via entity framework.";

        public void Execute(RequestPostsLikeDto request)
        {
            var result = this._validator.Validate(request);
            if (result.IsValid)
            {
                var postsLike = this.context.LikedPosts.FirstOrDefault(x => x.PostId == request.PostId && x.UserId == request.UserId);
                if(postsLike == null)
                {
                    throw new EntityNotFoundException("LikedPost", new {postId = request.PostId, userId = request.UserId});
                }
                this.context.LikedPosts.Remove(postsLike);
                this.context.SaveChanges();
            }else
            {
                throw new UnproccessableEntityException(result.Errors);
            }
        }
    }
}
