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
    public class CategoryValidator : AbstractValidator<CategoryDto>
    {
        private BlogAppDbContext _context;
        public CategoryValidator()
        {
            this._context = new BlogAppDbContext();
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name must not be null or empty")
                .MinimumLength(3).WithMessage("Minimum number of characters is 3.")
                .Matches("^[A-Z][a-z]{2,19}$").WithMessage("It only needs to contain letters and to start with a uppercase letter.")
                .Must(name => !this._context.Categories.Any(c => c.Name == name)).WithMessage("There is aleady a category with given name.");
        }
    }
}
