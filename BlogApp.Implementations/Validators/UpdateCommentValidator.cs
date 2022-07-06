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
    public class UpdateCommentValidator : AbstractValidator<CommentDto>
    {
        public UpdateCommentValidator(BlogAppDbContext context)
        {
            RuleFor(x => x.Id).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Comment id must not be null")
                .Must(id => context.Comments.Any(c => c.Id == id)).WithMessage("There is no given comment.");

            
            RuleFor(x => x.Content)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Content must not be null or empty");
                
        }
    }
}
