using BlogApp.Application.Dto;
using BlogApp.Application.Dto.searches;
using BlogApp.Application.UseCases;
using BlogApp.Application.UseCases.Commands;
using BlogApp.Application.UseCases.Queries;
using BlogApp.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Query
{
    public class EfGetPostsQuery : EfBase, IGetPostsQuery
    {
        public EfGetPostsQuery(BlogAppDbContext context) : base(context)
        {
        }

        public string Name => "EfGetPosts";

        public string Description => "Get all posts with optional search via entity framework.";

        public int Id => 11;

        public SearchResult<SearchResultPostDto> Execute(SearchPostDto request)
        {
            var query = this.context.Posts.Where(p => p.IsActive).Include(p => p.Author).Include(p => p.Category).Include(p => p.Comments).AsQueryable();

            var response = new SearchResult<SearchResultPostDto>();
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(p => p.Title.Contains(request.Keyword));
            }

            if(request.Page == null || request.Page.Value < 1)
            {
                request.Page = 1;
            }

            if(request.PerPage == null || request.PerPage.Value < 1)
            {
                request.Page = 20;
            }

            var postsToSkip = (request.Page.Value - 1) * request.PerPage.Value;

            response.TotalCount = query.Count();
            response.Data =  query.Skip(postsToSkip).Take(request.PerPage.Value).Select(p => new SearchResultPostDto
            {
                Id = p.Id,
                Content = p.Content,
                CategoryId = p.CategoryId,
                Title = p.Title,
                UserId = p.UserId,
                AuthorImage = p.Author.ProfilePicture,
                Author = p.Author.Firstname + " " + p.Author.Lastname ,
               Images = p.PostsImages.Where(i => i.IsActive).Select(x => new PostImageDto
               {
                    Id = x.Id,
                    PostId = x.PostId,
                    Source = x.Source
               }).ToList(),
               Comments = p.Comments.Where(c => c.IsActive).Select(x => new CommentDto
               {
                    Id = x.Id,
                    Content = x.Content,
                    ParentId = x.ParentId,
                    UserId = x.UserId,
                    PostId = x.PostId
               }).ToList()
            }) .ToList();
            response.CurrentPage = request.Page.Value;
            response.ItemsPerPage = request.PerPage.Value;

            return response;
        }

         
    }
}
