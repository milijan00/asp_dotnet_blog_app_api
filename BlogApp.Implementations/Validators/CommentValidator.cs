using BlogApp.Application.Dto;
using BlogApp.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.Validators
{
    public class CommentValidator : AbstractValidator<CommentDto>
    {
        private BlogAppDbContext _context;
        public CommentValidator(BlogAppDbContext context)
        {
            _context = context;
            RuleFor(c => c.Content).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Content must not be null or empty.");
            RuleFor(c => c.UserId).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("User id must not be null.")
                .Must(id => this._context.Users.Any(u => u.Id == id)).WithMessage("Given user doesn't exist.");
            RuleFor(c => c.PostId).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Post id must not be null.")
                .Must(id => this._context.Posts.Any(u => u.Id == id)).WithMessage("Given post doesn't exist.");
            RuleFor(c => c.ParentId).Cascade(CascadeMode.Stop)
                .Must(id => this._context.Comments.Any(c => c.Id == id)).WithMessage("Given comment doesn't exist.").When(c => c.ParentId.HasValue);
        }

    }
}
