using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Commands;
using BlogApp.DataAccess;
using BlogApp.Domain;
using BlogApp.Implementations.Exceptions;
using BlogApp.Implementations.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Command
{
    public class EfCreatePostsLikeCommand : EfBase, ICreatePostsLikeCommand
    {
        private CreatePostsLikeValidator _validator;
        public EfCreatePostsLikeCommand(BlogAppDbContext context, CreatePostsLikeValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 28;

        public string Name => "EfCratePostsLike";

        public string Description => "Like a post via entity framework.";

        public void Execute(RequestPostsLikeDto request)
        {
            var result = this._validator.Validate(request);
            if (result.IsValid)
            {
                var likedPost = new LikedPosts
                {
                    UserId = request.UserId,
                    PostId = request.PostId
                };
                this.context.LikedPosts.Add(likedPost);
                this.context.SaveChanges();
            }else
            {
                throw new UnproccessableEntityException(result.Errors);
            }
        }
    }
}
