using BlogApp.Application.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.Validators
{
    public abstract class BaseNavigationLinkValidator : AbstractValidator<NavigationLInkDto>
    {
        protected string NameRegEx => @"^[A-z\s]{3,20}$";
        protected string RouteRegEx => @"^[a-z\s]{3,20}$";
        public BaseNavigationLinkValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Name must not be empty or null.")
                .MinimumLength(3).WithMessage("Minimum length is 3 characters.")
                .MaximumLength(20).WithMessage("Maximum length is 20 characters.")
                .Matches(NameRegEx).WithMessage("First capital, no numbers nor special characters.");
            this.AppendNameConfigurationRules();

            RuleFor(x => x.Route)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Route must not be empty or null.")
                .MinimumLength(3).WithMessage("Minimum length is 3 characters.")
                .MaximumLength(20).WithMessage("Maximum length is 20 characters.")
                .Matches(RouteRegEx).WithMessage("lowercase letters, no numbers nor special characters.");
            this.AppendRoleConfigurationRules();
        }
        protected abstract void AppendNameConfigurationRules();
        protected abstract void AppendRoleConfigurationRules();
    }
}
