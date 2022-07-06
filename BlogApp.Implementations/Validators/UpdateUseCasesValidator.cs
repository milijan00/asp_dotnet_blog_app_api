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
    public class UpdateUseCasesValidator : AbstractValidator<UpdateUseCasesDto>
    {
        public UpdateUseCasesValidator(BlogAppDbContext context)
        {
            RuleFor(x => x.UserId)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("User id must not be null.")
                .Must(id => context.Roles.Any(u => u.Id == id)).WithMessage("There is no such user.");

            RuleFor(x => x.UseCasesIds)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("UseCases must not be null.")
                .Must(useCases => useCases.Distinct().Count() == useCases.Count()).WithMessage("Duplicates are not allowed.");
        }
    }
}
