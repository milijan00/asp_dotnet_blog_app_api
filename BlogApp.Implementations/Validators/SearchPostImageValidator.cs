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
    public class SearchPostImageValidator : AbstractValidator<SearchPostImageDto>
    {
        private BlogAppDbContext _context;
        public SearchPostImageValidator(BlogAppDbContext context)
        {
            _context = context;

            RuleFor(x => x.PostId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Post id must not be null.")
                .Must(id => this._context.Posts.Any(p => p.Id == id)).WithMessage("Given post doesn't exist;");
        }
        
    }
}
