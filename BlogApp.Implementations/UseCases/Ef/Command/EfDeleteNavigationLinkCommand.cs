using BlogApp.Application.UseCases.Commands;
using BlogApp.DataAccess;
using BlogApp.DataAccess.Extensions;
using BlogApp.Domain;
using BlogApp.Implementations.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Command
{
    public class EfDeleteNavigationLinkCommand : EfBase, IDeleteNavigationLinkCommand
    {
        public EfDeleteNavigationLinkCommand(BlogAppDbContext context) : base(context)
        {
        }

        public int Id => 38;

        public string Name =>  "EfDeleteNavigationLinkCommand";

        public string Description => "";

        public void Execute(int id)
        {
            var link = this.context.NavigationLinks.Find(id);

            if(link != null)
            {
                this.context.Deactivate(link);
                this.context.SaveChanges();
            }else
            {
                throw new EntityNotFoundException(nameof(NavigationLink), id);
            }
        }
    }
}
