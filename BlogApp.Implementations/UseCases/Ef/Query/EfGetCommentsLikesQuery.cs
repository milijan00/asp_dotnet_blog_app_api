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
    public class EfGetCommentsLikesQuery : EfBase, IGetCommentsLikesQuery
    {
        private SearchCommentLikesValidator _validator;
        public EfGetCommentsLikesQuery(BlogAppDbContext context, SearchCommentLikesValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 22;

        public string Name => "EfGetCommentsLikes";

        public string Description => "Get all likes of a comment via entity framework.";

        public LikedCommentDto Execute(SearchForCommentsLikesDto request)
        {
            var result = this._validator.Validate(request);
            if (result.IsValid)
            {
                var comment = this.context.Comments
                    .Include(c => c.UsersWhoLiked).ThenInclude(lc => lc.User)
                    .FirstOrDefault(c => c.Id == request.CommentId);
                return new LikedCommentDto
                {
                    CommentId = comment.Id,
                    usersWhoLiked = comment.UsersWhoLiked.Select(x => new UserWhoLikedDto
                    {
                        UserId = x.UserId,
                        Username = x.User.Username
                    }).ToList()
                };
            }else
            {
                throw new UnproccessableEntityException(result.Errors);
            }
        }
    }
}
