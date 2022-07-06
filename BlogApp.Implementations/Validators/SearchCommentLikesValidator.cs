using BlogApp.Application.Dto.searches;
using BlogApp.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.Validators
{
    public class SearchCommentLikesValidator : AbstractValidator<SearchForCommentsLikesDto>
    {
        private BlogAppDbContext _context;
        public SearchCommentLikesValidator(BlogAppDbContext context)
        {
            this._context = context;
            RuleFor(x => x.CommentId).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Comment id must not be null.")
                .Must(id => this._context.Comments.Any(c => c.Id == id)).WithMessage("There is no comment with such id.");
        }
    }
}
