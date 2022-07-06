using BlogApp.Application.UseCases.Commands;
using BlogApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Command
{
    public class EfDeleteCategoryCommand : EfBase, IDeleteCategoryCommand
    {
        public EfDeleteCategoryCommand(BlogAppDbContext context) : base(context)
        {
        }

        public string Name => "EfDeleteCategoryCommand";

        public string Description => "Delete a category via entity framework";

        public int Id => 5;

        public void Execute(int request)
        {

            var category = this.context.Categories.Find(request);
            this.context.Categories.Remove(category);
            this.context.SaveChanges();
        }
    }
}
