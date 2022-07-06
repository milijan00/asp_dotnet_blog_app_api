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
    public class EfCreateLikedCommentCommand : EfBase, ICreateLikedCommentCommand
    {
        private CreateLikedCommentValidator _validator;
        public EfCreateLikedCommentCommand(BlogAppDbContext context, CreateLikedCommentValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 23;

        public string Name => "EfCreateLikedComment";

        public string Description => "Like a comment via entity framework.";

        public void Execute(RequestLikedCommentDto request)
        {
            var result = this._validator.Validate(request);
            if (result.IsValid)
            {
                var likedcomment = new LikedComment
                {
                    UserId = request.UserId,
                    CommentId = request.CommentId
                };
                this.context.LikedComments.Add(likedcomment);
                this.context.SaveChanges();
            } else
            {
                throw new UnproccessableEntityException(result.Errors);
            }
        }
    }
}
