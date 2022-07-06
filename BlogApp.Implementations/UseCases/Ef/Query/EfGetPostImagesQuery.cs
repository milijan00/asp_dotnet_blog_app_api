using BlogApp.Application.Dto;
using BlogApp.Application.Dto.searches;
using BlogApp.Application.UseCases.Queries;
using BlogApp.DataAccess;
using BlogApp.Implementations.Exceptions;
using BlogApp.Implementations.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Query
{
    public class EfGetPostImagesQuery : EfBase, IGetPostImagesQuery
    {
        private SearchPostImageValidator _validator;
        public EfGetPostImagesQuery(BlogAppDbContext context, SearchPostImageValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 24;

        public string Name => "EfGetPostImages";

        public string Description => "Get post images via entity framework.";

        public IEnumerable<PostImageDto> Execute(SearchPostImageDto request)
        {
            var result = this._validator.Validate(request);

            if (result.IsValid)
            {
                var post = this.context.Posts.Include(p => p.PostsImages).FirstOrDefault(p => p.Id == request.PostId.Value);
                return post.PostsImages.Where(pi => pi.IsActive).Select(p => new PostImageDto
                {
                    PostId = p.PostId,
                    Id = p.Id,
                    Source = p.Source
                }).ToList();
            }
            else
            {
                throw new UnproccessableEntityException(result.Errors);
            }
        }
    }
}
