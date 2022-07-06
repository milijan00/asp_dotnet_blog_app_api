using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Commands;
using BlogApp.DataAccess;
using BlogApp.DataAccess.Extensions;
using BlogApp.Implementations.Exceptions;
using BlogApp.Implementations.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Command
{
    public class EfUpdatePostImagesCommand : EfBase, IUpdatePostImagesCommand
    {
        private UpdatePostImagesValidator _validator;
        public EfUpdatePostImagesCommand(BlogAppDbContext context, UpdatePostImagesValidator validator) : base(context)
        {
            this._validator = validator;
        }

        public int Id => 34;

        public string Name => "EfUpdatePostImagesCommand";

        public string Description => "";

        public void Execute(UpdatePostImagesDto request)
        {
            var result = this._validator.Validate(request);
            if (result.IsValid)
            {
                var images = this.context.PostsImages.Where(p => p.PostId == request.PostId && p.IsActive).ToList();
                this.context.DeactivateRange(images);
                images = request.Images.Select(x => new Domain.PostsImage
                {
                    PostId = request.PostId,
                    Source = Guid.NewGuid().ToString() + x
                }).ToList();
                this.context.PostsImages.AddRange(images);
                this.context.SaveChanges();
            }else
            {
                throw new UnproccessableEntityException(result.Errors);
            }
        }
    }
}
