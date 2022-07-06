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
    public class UpdatePostImagesValidator : BasePostImageValidator<UpdatePostImagesDto>
    {
        public UpdatePostImagesValidator(BlogAppDbContext context)
        {
            RuleFor(x => x.PostId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Post id must not be null.")
                .Must(id => context.Posts.Any(p => p.Id == id)).WithMessage("There is no such post.");

            RuleFor(x => x.Images)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Images must not be null or empty.")
                .Must(images => images.Count() == images.Distinct().Count()).WithMessage("Duplicates are not allowed.");

            RuleForEach(x => x.Images).Matches(this.ImageRegEx).WithMessage($"Minimum length is 1 and maximum is 50. Image names can contain letters, number and special characters such as ., _, -. Acceptable image extensions are {ExtensionsInOneRow}");
        }
    }
}
