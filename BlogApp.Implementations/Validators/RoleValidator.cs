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
    public class RoleValidator : AbstractValidator<RoleDto>
    {
        private BlogAppDbContext _context;
        public RoleValidator()
        {
            this._context = new BlogAppDbContext();
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name must not be null.")
                .MinimumLength(3).WithMessage("Minimum number of characters is 3.")
                .Matches("^[A-Z][a-z]{2,19}$").WithMessage("It only needs to contain letters and starts with a uppercase letter.")
                .Must(name => !this._context.Roles.Any(r => r.Name == name)).WithMessage("There is already a role with given name.");
        }
    }
}
