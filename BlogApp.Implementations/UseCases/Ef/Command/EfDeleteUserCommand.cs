using BlogApp.Application.UseCases.Commands;
using BlogApp.DataAccess;
using BlogApp.DataAccess.Extensions;
using BlogApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Command
{
    public class EfDeleteUserCommand : EfBase, IDeleteUserCommand
    {
        public EfDeleteUserCommand(BlogAppDbContext context) : base(context)
        {
        }

        public string Name => "EfDeleteUser";

        public string Description => "Delete a specific user via entity framework.";

        public int Id => 7;

        public void Execute(int request)
        {
            this.context.Deactivate<User>(request);
            this.context.SaveChanges();
        }
    }
}
