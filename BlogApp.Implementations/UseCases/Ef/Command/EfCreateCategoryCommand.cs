using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Commands;
using BlogApp.DataAccess;
using BlogApp.Domain;
using BlogApp.Implementations.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Command
{
    public class EfCreateCategoryCommand : EfBase,  ICreateCategoryCommand
    {
        private CategoryValidator _validator;
        public EfCreateCategoryCommand(BlogAppDbContext context, CategoryValidator validator) : base(context)
        {
            this._validator = validator;
        }

        public string Name => "EfCreateCategoryCommand";

        public string Description => "Create a category via entity framework";

        public int Id => 1;
        public void Execute(CategoryDto request)
        {
            var result = this._validator.Validate(request);
            if (result.IsValid)
            {
            var category = new Category
            {
                Name = request.Name
            };
                Console.WriteLine("sve je u redu");
                this.context.Categories.Add(category);
                this.context.SaveChanges();
            }
            else
            {
                result.Errors.ForEach(x => Console.WriteLine($"{x.PropertyName} : {x.ErrorMessage}"));
            }
        }
    }
}
