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
    public class DeletePostsImagesValidator : AbstractValidator<DeletePostsImagesDto>
    {
        private BlogAppDbContext _context;

        public DeletePostsImagesValidator(BlogAppDbContext context)
        {
            _context = context;

            RuleFor(x => x.ImagesIds).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Array must not be null or empty.")
                .Must(ids => ids.Distinct().Count() == ids.Count()).WithMessage("There are some duplicates in the array.")
                .Must(ids => this._context.PostsImages.Where(pi => ids.Any(id => id == pi.Id)).Count() == ids.Count()).WithMessage("One or more ids doesn't exist.");
        }
    }
}
