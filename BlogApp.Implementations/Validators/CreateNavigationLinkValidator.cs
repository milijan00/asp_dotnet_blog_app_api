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
    public class CreateNavigationLinkValidator : BaseNavigationLinkValidator 
    {
        private BlogAppDbContext _context;
        public CreateNavigationLinkValidator(BlogAppDbContext context)
        {
            this._context = context;
        }

        protected override void AppendNameConfigurationRules()
        {
            RuleFor(x => x.Name).Must(name => !this._context.NavigationLinks.Any(n => n.Name == name))
                .WithMessage("This link is already in the database.");
        }

        protected override void AppendRoleConfigurationRules()
        {
            RuleFor(x => x.Route).Must(route => !this._context.NavigationLinks.Any(n => n.Route == route))
                .WithMessage("This link is already in the database.");
        }
    }
}
