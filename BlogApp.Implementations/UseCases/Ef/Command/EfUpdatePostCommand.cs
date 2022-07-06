using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Commands;
using BlogApp.DataAccess;
using BlogApp.Implementations.Exceptions;
using BlogApp.Implementations.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Command
{
    public class EfUpdatePostCommand : EfBase, IUpdatePostCommand
    {
        private UpdatePostValidator _validator;
        public EfUpdatePostCommand(BlogAppDbContext context, UpdatePostValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 33;

        public string Name => "EfUpdatePostCommand";

        public string Description => "Update a post via entity framework.";

        public void Execute(UpdatePostDto request)
        {
            var result = this._validator.Validate(request);
            if(result.IsValid)
            {
                var post = this.context.Posts.Find(request.Id);
                if (!string.IsNullOrEmpty(request.Title))
                {
                    post.Title = request.Title; 
                }

                if (!string.IsNullOrEmpty(request.Content))
                {
                    post.Content = request.Content;
                }

                if (request.CategoryId.HasValue)
                {
                    post.CategoryId = request.CategoryId.Value;
                }
                this.context.SaveChanges();
            }else
            {
                throw new UnproccessableEntityException(result.Errors);
            }
        }
    }
}
