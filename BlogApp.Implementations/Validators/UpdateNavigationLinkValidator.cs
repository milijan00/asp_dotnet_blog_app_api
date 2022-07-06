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
    public class UpdateNavigationLinkValidator : BaseNavigationLinkValidator 
    {
        private BlogAppDbContext _context;
        public UpdateNavigationLinkValidator(BlogAppDbContext context)
        {
            this._context = context; 
        }

        protected override void AppendNameConfigurationRules()
        {
        }

        protected override void AppendRoleConfigurationRules()
        {
        }
    }
}
