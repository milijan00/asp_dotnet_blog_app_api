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
    public class CreatePostImageValidator : BasePostImageValidator<CreatePostImageDto>
    {
        private BlogAppDbContext _context;

        public CreatePostImageValidator(BlogAppDbContext context)
        {
            _context = context;
            RuleFor(x => x.PostId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Post id must not be null.")
                .Must(id => this._context.Posts.Any(p => p.Id == id)).WithMessage("The given post doesn't exist");

            RuleFor(x => x.Images)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Images array must not be empty or null.")
                .Must(sources => sources.Count() >= 1).WithMessage("You have to send at least one image.")
                .Must(sources => sources.Distinct().Count() == sources.Count()).WithMessage("There are some duplicates in the array.");

            RuleForEach(x => x.Images).Matches(this.ImageRegEx).WithMessage($"Minimum length is 1 and maximum is 50. Image names can contain letters, number and special characters such as ., _, -. Acceptable image extensions are {ExtensionsInOneRow}");
        }
    }
}
