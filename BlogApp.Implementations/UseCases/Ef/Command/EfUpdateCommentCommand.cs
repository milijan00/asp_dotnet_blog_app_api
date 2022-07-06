using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Commands;
using BlogApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApp.Implementations.Exceptions;
using BlogApp.Domain;
using BlogApp.Implementations.Validators;

namespace BlogApp.Implementations.UseCases.Ef.Command
{
    public class EfUpdateCommentCommand : EfBase, IUpdateCommentCommand
    {
        private readonly UpdateCommentValidator _validator;
        public EfUpdateCommentCommand(BlogAppDbContext context, UpdateCommentValidator validator) : base(context)
        {
            this._validator = validator;
        }

        public int Id => 30;

        public string Name => "EfUpdateCommentCommand";

        public string Description => "";

        public void Execute(CommentDto request)
        {
            var result = this._validator.Validate(request);
            if (result.IsValid)
            {
                var comment = this.context.Comments.Find(request.Id);
                comment.Content = request.Content;
                this.context.SaveChanges();
            }
            else {
                throw new UnproccessableEntityException(result.Errors);
            }
            
        }

    }
}
