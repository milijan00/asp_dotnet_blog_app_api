using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApp.DataAccess;
namespace BlogApp.Implementations.UseCases.Ef.Command
{
    public class EfUpdateRoleCommand : EfBase,  IUpdateRoleCommand
    {
        
        public EfUpdateRoleCommand(BlogAppDbContext context)  : base(context) { }
        public string Name => "EfUpdateRole";

        public string Description => "Update a specific role";

        public int Id =>15;

        public void Execute(RoleDto request)
        {
            var role = this.context.Roles.Find(request.Id.Value);
            role.Name = request.Name;
            this.context.SaveChanges();
        }
    }
}
