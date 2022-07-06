using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Queries;
using BlogApp.DataAccess;
using BlogApp.Domain;
using BlogApp.Implementations.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Query
{
    public class EfFindNavigationLinkQuery : EfBase, IFindNavigationLinkQuery
    {
        public EfFindNavigationLinkQuery(BlogAppDbContext context) : base(context)
        {
        }

        public int Id => 37;

        public string Name => "EfFindNavigationLinkQuery";

        public string Description => "";

        public NavigationLInkDto Execute(int id)
        {
            var navigationLink = this.context.NavigationLinks.FirstOrDefault(n => n.Id == id && n.IsActive);
            if(navigationLink == null)
            {
                throw new EntityNotFoundException(nameof(NavigationLink), id);
            }

            return new NavigationLInkDto { 
                Id = navigationLink.Id,
                Name = navigationLink.Name,
                Route = navigationLink.Route
            };

        }
    }
}
