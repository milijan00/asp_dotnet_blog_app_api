using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Commands;
using BlogApp.DataAccess;
using BlogApp.Implementations.Exceptions;
using BlogApp.Implementations.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Command
{
    public class EfUpdateNavigationLinkCommand : EfBase, IUpdateNavigationLinkCommand
    {
        private UpdateNavigationLinkValidator _validator;
        public EfUpdateNavigationLinkCommand(BlogAppDbContext context, UpdateNavigationLinkValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 39;

        public string Name => "EfUpdateNavigationLinkCommand";

        public string Description => "";

        public void Execute(NavigationLInkDto request)
        {
            var result = this._validator.Validate(request);
            if (result.IsValid)
            {
                var link = this.context.NavigationLinks.FirstOrDefault(n => n.Id == request.Id && n.IsActive);
                link.Name = request.Name;
                link.Route = request.Route;

                this.context.SaveChanges();
            }else
            {
                throw new UnproccessableEntityException(result.Errors);
            }
        }
    }
}
