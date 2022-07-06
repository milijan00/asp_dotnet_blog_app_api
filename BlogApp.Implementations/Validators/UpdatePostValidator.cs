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
    public class UpdatePostValidator : AbstractValidator<UpdatePostDto>
    {
        public UpdatePostValidator(BlogAppDbContext context)
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Post id must not be null.")
                .Must(id => context.Posts.Any(p => p.Id == id)).WithMessage("There is no such post.");

            RuleFor(x => x.Title).Cascade(CascadeMode.Stop)
                .MinimumLength(2).WithMessage("Minimum number of characters is 2.").When(x => x.Title != null)
                .MaximumLength(30).WithMessage("Maximum number of characters is 30.")
                .Matches(@"^[A-z0-9\s]{2,}$");

            RuleFor(x => x.Content).Cascade(CascadeMode.Stop)
                .MinimumLength(2).WithMessage("Minimum number of characters is 2.").When(x => x.Content != null);

            //RuleFor(x => x.UserId).Cascade(CascadeMode.Stop)
            //    .NotEmpty().WithMessage("User id must not be null.")
            //    .Must(id => this._context.Users.Any(u => u.Id == id && u.IsActive)).WithMessage("Given user doesn't exist.");

            RuleFor(x => x.CategoryId).Cascade(CascadeMode.Stop)
                .Must(id => context.Categories.Any(c => c.Id == id && c.IsActive)).WithMessage("Given category doesn't exist.").When(x => x.CategoryId.HasValue);
        }
    }
}
