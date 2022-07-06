using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Queries;
using BlogApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Query
{
    public class EfGetNavigationLinksQuery : EfBase, IGetNavigationLinksQuery
    {
        public EfGetNavigationLinksQuery(BlogAppDbContext context) : base(context)
        {
        }

        public int Id => 35;

        public string Name => "EfGetNavigationLinksQuery";

        public string Description => "";

        public IEnumerable<NavigationLInkDto> Execute()
        {
            return this.context.NavigationLinks.Where(n => n.IsActive).Select(x => new NavigationLInkDto
            {
                Id = x.Id,
                Name = x.Name,
                Route = x.Route
            }).ToList();
        }
    }
}
