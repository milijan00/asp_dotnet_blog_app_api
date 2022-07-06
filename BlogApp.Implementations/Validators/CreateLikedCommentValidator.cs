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
    public  class CreateLikedCommentValidator : AbstractValidator<RequestLikedCommentDto>
    {
        private BlogAppDbContext _context;
        public CreateLikedCommentValidator(BlogAppDbContext context)
        {
            _context = context;

            RuleFor(x => x.CommentId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Comment id must not be null.")
                .Must(commentId => this._context.Comments.Any(c => c.Id == commentId)).WithMessage("Given comment doesn't exist.");
            RuleFor(x => x.UserId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("User id must not be null.")
                .Must(userId => this._context.Users.Any(u => u.Id == userId)).WithMessage("Given user doesn't exist.");

            RuleFor(x => new { cId = x.CommentId, uId = x.UserId })
                .Must(dto => !this._context.LikedComments.Any(lc => lc.CommentId == dto.cId && lc.UserId == dto.uId)).WithMessage("The user has already liked the comment.");

        }
    }
}
