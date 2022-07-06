using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Commands;
using BlogApp.DataAccess;
using BlogApp.DataAccess.Extensions;
using BlogApp.Domain;
using BlogApp.Implementations.Exceptions;
using BlogApp.Implementations.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Command
{
    public class EfDeletePostsImagesCommand : EfBase, IDeletePostsImagesCommand
    {
        private DeletePostsImagesValidator _validator;
        public EfDeletePostsImagesCommand(BlogAppDbContext context, DeletePostsImagesValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 26;

        public string Name => "EfDeletePostsImages";

        public string Description => "Delete posts images via entity framework.";

        public void Execute(DeletePostsImagesDto request)
        {
            var result = this._validator.Validate(request);
            if (result.IsValid)
            {
                this.context.Deactivate<PostsImage>(request.ImagesIds);
                this.context.SaveChanges();
            }
            else
            {
                throw new UnproccessableEntityException(result.Errors);
            }
        }
    }
}
