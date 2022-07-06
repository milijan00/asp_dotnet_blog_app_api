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
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator(BlogAppDbContext context)
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("User id must not be null")
                .Must(id => context.Users.Any(u => u.Id == id.Value)).WithMessage("There is no such user.");

            RuleFor(x => x.Firstname).Cascade(CascadeMode.Stop)
                .MinimumLength(3).WithMessage("Minimum length is 3 characters.").When(x => x.Firstname != null)
                .MaximumLength(30).WithMessage("Maximum length is 30 characters")
                .Matches(@"^[A-Z][a-z]{2,}(\s[A-Z][a-z]{2,})?$").WithMessage("It must contain letters only.");

            RuleFor(x => x.Lastname).Cascade(CascadeMode.Stop)
                .MinimumLength(3).WithMessage("Minimum length is 3 characters.").When(x => x.Lastname != null)
                .MaximumLength(30).WithMessage("Maximum length is 30 characters")
                .Matches(@"^[A-Z][a-z]{2,}(\s[A-Z][a-z]{2,})?$").WithMessage("It must contain letters only.");

            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
                .MaximumLength(50).WithMessage("Maximum length is 50.") .When(x => x.Email != null)
                .Must(email => !context.Users.Any(u => u.Email == email));

            RuleFor(x => x.Username).Cascade(CascadeMode.Stop)
                .MinimumLength(2).WithMessage("Minimum length is 2.").When(x => x.Username != null)
                .MaximumLength(20).WithMessage("Maximum length is 20.")
                .Matches(@"^[A-z0-9\-_\.]{2,}$").WithMessage("It can contain letters, numbers and several special characters (-, _, and .).")
                .Must(username => !context.Users.Any(u => u.Username == username)).WithMessage("The given username has been taken.");

            RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
                .MinimumLength(8).WithMessage("Minimum length is 8.").When(x => x.Password != null)
                .MaximumLength(50).WithMessage("Maximum length is 50.")
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$").WithMessage("Minimum 8 characters, maximum is 50. It needs at least one letter and one number.");

            RuleFor(x => x.ProfilePicture).Matches(@"^[A-z0-9\-\._]{1,50}\.(jpg|png|jpeg)$")
                .WithMessage($"Minimum length is 1 and maximum is 50. Image names can contain letters, number and special characters such as ., _, -. Acceptable image extensions are .jpg, .png and .jpeg.")
                .When(x => x.ProfilePicture != null);
        }
    }
}
