using AutoMapper;
using BlogApp.Application.Dto;
using BlogApp.Application.Dto.searches;
using BlogApp.Application.UseCases.Queries;
using BlogApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Query
{
    public class EfGetCommentsQuery : EfBase, IGetCommentsQuery
    {
        private IMapper _mapper;
        public EfGetCommentsQuery(BlogAppDbContext context,IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 17; 

        public string Name => "EfGetComments";

        public string Description => "Search for comments via entity framework";

        public IEnumerable<CommentDto> Execute(SearchCommentDto request)
        {
            var query = this.context.Comments.Where(c => c.IsActive).AsQueryable();
            if (request.UserId.HasValue)
            {
                query = query.Where(c => c.UserId == request.UserId.Value);
            }
            if (request.PostId.HasValue)
            {
                query = query.Where(c => c.PostId == request.PostId.Value);
            }
            var comments = query.ToList();
            return this._mapper.Map<List<CommentDto>>(comments);
        }
    }
}
