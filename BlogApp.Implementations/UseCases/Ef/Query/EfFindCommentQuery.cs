using AutoMapper;
using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Queries;
using BlogApp.DataAccess;
using BlogApp.Domain;
using BlogApp.Implementations.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Query
{
    public class EfFindCommentQuery : EfBase, IFindCommentQuery
    {
        private IMapper _mapper;
        public EfFindCommentQuery(BlogAppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 18;

        public string Name => "EfFindComment";

        public string Description => "Get a specific comment via entity framework.";

        public CommentDto Execute(int request)
        {
            var comment = this.context.Comments.Find(request);
            if(comment == null)
            {
                throw new EntityNotFoundException(nameof(Comment), request);
            }

            return this._mapper.Map<CommentDto>(comment);
        }
    }
}
