using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Commands;
using BlogApp.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.Validators
{
    public class CreatePostsLikeValidator : AbstractValidator<RequestPostsLikeDto>
    {
        private BlogAppDbContext _context;
        public CreatePostsLikeValidator(BlogAppDbContext context)
        {
            _context = context;

            RuleFor(x => x.UserId).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("User id must not be null.")
                .Must(id => this._context.Users.Any(u => u.Id == id)).WithMessage("Given user doesn't exist.");

            RuleFor(x => x.PostId).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Post id must not be null.")
                .Must(id => this._context.Posts.Any(u => u.Id == id)).WithMessage("Given post doesn't exist.");

            RuleFor(x => new { postId = x.PostId, userId = x.UserId})
                .Must(dto => !this._context.LikedPosts.Any(lp => lp.PostId == dto.postId && lp.UserId == dto.userId))
                .WithMessage("Given user has already liked the post.");
        }
    }
}
