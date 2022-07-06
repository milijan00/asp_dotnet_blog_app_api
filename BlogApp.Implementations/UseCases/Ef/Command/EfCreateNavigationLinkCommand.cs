using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Commands;
using BlogApp.DataAccess;
using BlogApp.Domain;
using BlogApp.Implementations.Exceptions;
using BlogApp.Implementations.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Command
{
    public class EfCreateNavigationLinkCommand : EfBase, ICreateNavigationLinkCommand
    {
        private readonly CreateNavigationLinkValidator _validator;
        public EfCreateNavigationLinkCommand(BlogAppDbContext context, CreateNavigationLinkValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 36;

        public string Name => "EfCreateNavigationLinkCommand";

        public string Description => "";

        public void Execute(NavigationLInkDto request)
        {
            var result = this._validator.Validate(request);
            if (result.IsValid)
            {
                var navigationLink = new NavigationLink
                {
                    Name = request.Name,
                    Route = request.Route
                };

                this.context.NavigationLinks.Add(navigationLink);
                this.context.SaveChanges();
            }else
            {
                throw new UnproccessableEntityException(result.Errors);
            }
        }
    }
}
