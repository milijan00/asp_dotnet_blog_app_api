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
    public class UserValidator : AbstractValidator<RegisteredUserDto>
    {
        private BlogAppDbContext _context;
        public UserValidator()
        {
            this._context = new BlogAppDbContext();

            RuleFor(x => x.Firstname).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Firstname must not be null or empty.")
                .MinimumLength(3).WithMessage("Minimum length is 3 characters.")
                .MaximumLength(30).WithMessage("Maximum length is 30 characters")
                .Matches(@"^[A-Z][a-z]{2,}(\s[A-Z][a-z]{2,})?$").WithMessage("It must contain letters only.");
            RuleFor(x => x.Lastname).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Lastname must not be null or empty.")
                .MinimumLength(3).WithMessage("Minimum length is 3 characters.")
                .MaximumLength(30).WithMessage("Maximum length is 30 characters")
                .Matches(@"^[A-Z][a-z]{2,}(\s[A-Z][a-z]{2,})?$").WithMessage("It must contain letters only.");
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email must not be null or empty.")
                .MaximumLength(50).WithMessage("Maximum length is 50.")// dodati proveru za mejl 
                .Must(email => !this._context.Users.Any(u => u.Email == email));
            RuleFor(x => x.Username).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Username must not be null or empty.")
                .MinimumLength(2).WithMessage("Minimum length is 2.")
                .MaximumLength(20).WithMessage("Maximum length is 20.")
                .Matches(@"^[A-z0-9\-_\.]{2,}$").WithMessage("It can contain letters, numbers and several special characters (-, _, and .).")
                .Must(username => !this._context.Users.Any(u => u.Username == username)).WithMessage("The given username has been taken.");
            RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Password must not be null or empty.")
                .MinimumLength(8).WithMessage("Minimum length is 8.")
                .MaximumLength(50).WithMessage("Maximum length is 50.")
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$").WithMessage("Minimum 8 characters, maximum is 50. It needs at least one letter and one number.");

        }
    }
}
