using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApp.Application.UseCases.Commands;
using BlogApp.DataAccess;
namespace BlogApp.Implementations.UseCases.Ef.Command
{
    public class EfDeleteRoleCommand : EfBase, IDeleteRoleCommand
    {
        public EfDeleteRoleCommand(BlogAppDbContext context) : base(context)
        {

        }
        public string Name => "EfDeleteRole";

        public string Description => "Delete a role with given id.";

        public int Id => 6;

        public void Execute(int id) {
            var role = this.context.Roles.Find(id);
            this.context.Roles.Remove(role);
            this.context.SaveChanges();
        }
    }
}
