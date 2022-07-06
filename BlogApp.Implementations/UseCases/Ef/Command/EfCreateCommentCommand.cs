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
    public class EfCreateCommentCommand : EfBase, ICreateCommentCommand
    {
        private CommentValidator _validator;
        public EfCreateCommentCommand(BlogAppDbContext context, CommentValidator validator ) : base(context)
        {
            _validator = validator;
        }

        public int Id => 19;

        public string Name => "EfCreateComment";

        public string Description => "Create a comment via entity framework.";

        public void Execute(CommentDto request)
        {
            var result = this._validator.Validate(request);

            if (result.IsValid)
            {
                var comment = new Comment
                {
                    PostId = request.PostId.Value,
                    UserId = request.UserId.Value,
                    ParentId = request.ParentId,
                    Content = request.Content
                };
                this.context.Comments.Add(comment);
                this.context.SaveChanges();
            } else
            {
                throw new UnproccessableEntityException(result.Errors);
            }
        }
    }
}
