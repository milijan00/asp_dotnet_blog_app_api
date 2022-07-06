using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Commands;
using BlogApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Command
{
    public class EfUpdateCategoryCommand : EfBase,  IUpdateCategoryCommand
    {

        public EfUpdateCategoryCommand(BlogAppDbContext context)
            :base(context)
        {
        }

        public string Name => "EfUpdateCategoryCommand";

        public string Description => "Update a category via entity framework";

        public int Id =>14;

        public void Execute(CategoryDto request)
        {
            var category = this.context.Categories.Find(request.Id.Value);

            category.Name = request.Name;
            this.context.SaveChanges();
        }
    }
}
