using BlogApp.Application.Dto;
using BlogApp.Application.Dto.searches;
using BlogApp.Application.UseCases.Queries;
using BlogApp.DataAccess;
using BlogApp.DataAccess.Exceptions;
using BlogApp.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Query
{
    public class EfFindPostQuery : EfBase, IFindPostQuery
    {
        public EfFindPostQuery(BlogAppDbContext context) : base(context)
        {
        }

        public int Id => 20;

        public string Name => "EfFindPost";

        public string Description => "Find a specific post via entity framework.";

        public SearchResultPostDto Execute(int id)
        {
            var post = this.context.Posts
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Include(p => p.Comments)
                .FirstOrDefault(p => p.Id == id && p.IsActive);
            if(post == null)
            {
                throw new EntityNotFoundException();
            }
            return new SearchResultPostDto
            {
                Id = post.Id,
                Content = post.Content,
                CategoryId = post.CategoryId,
                Title = post.Title,
                UserId = post.UserId,
                AuthorImage = post.Author.ProfilePicture,
               Author = post.Author.Firstname + " " + post.Author.Lastname ,
               Images = post.PostsImages.Select(x => new PostImageDto
               {
                    Id = x.Id,
                    PostId = x.PostId,
                    Source = x.Source
               }).ToList()
            };

        }
    }
}
