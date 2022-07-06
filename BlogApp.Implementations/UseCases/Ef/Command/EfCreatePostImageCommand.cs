using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Commands;
using BlogApp.DataAccess;
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
    public class EfCreatePostImageCommand : EfBase, ICreatePostImageCommand
    {
        private CreatePostImageValidator _validator;
        public EfCreatePostImageCommand(BlogAppDbContext context, CreatePostImageValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 25;

        public string Name => "EfCreatePostImage";

        public string Description => "Insert a post image via entityFramework";

        public void Execute(CreatePostImageDto request)
        {
            var result = this._validator.Validate(request);
            if (result.IsValid)
            {
                var postImages = request.Images.Select(image => new PostsImage
                {
                    PostId = request.PostId.Value,
                    Source = Guid.NewGuid().ToString() + image
                }).ToList();

                this.context.PostsImages.AddRange(postImages);
                this.context.SaveChanges();
            }
            else
            {
                throw new UnproccessableEntityException(result.Errors);
            }
        }
    }
}
